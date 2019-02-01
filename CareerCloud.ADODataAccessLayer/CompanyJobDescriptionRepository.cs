using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
	public class CompanyJobDescriptionRepository : BaseADO,IDataRepository<CompanyJobDescriptionPoco>
	{
		public void Add(params CompanyJobDescriptionPoco[] items)
		{
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand command = new SqlCommand();
				command.Connection = conn;
				foreach (CompanyJobDescriptionPoco poco in items)
				{
					command.CommandText = @"Insert into [dbo].[Company_Jobs_Descriptions]
                     ([Id],[Job],[Job_Name],[Job_Descriptions])
						Values (@Id,@Job,@Job_Name,@Job_Descriptions)";

					command.Parameters.AddWithValue("@Id", poco.Id);
					command.Parameters.AddWithValue("@Job", poco.Job);
					command.Parameters.AddWithValue("@Job_Name", poco.JobName);
					command.Parameters.AddWithValue("@Job_Descriptions", poco.JobDescriptions);
					conn.Open();
					int rowEffected = command.ExecuteNonQuery();
					conn.Close();
				}

			}
		}

		public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
		{
			throw new NotImplementedException();
		}

		public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
		{
			CompanyJobDescriptionPoco[] pocos = new CompanyJobDescriptionPoco[1001];
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand command = new SqlCommand("Select * from Company_Jobs_Descriptions", conn);

				int position = 0;
				conn.Open();
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					CompanyJobDescriptionPoco poco = new CompanyJobDescriptionPoco();
					poco.Id = reader.GetGuid(0);
					poco.Job = reader.GetGuid(1);
					if (!reader.IsDBNull(2))
					{
						poco.JobName = reader.GetString(2);
					}
					else
					{
						poco.JobName = null;
					}
					if (!reader.IsDBNull(3))
					{
						poco.JobDescriptions = reader.GetString(3);
					}
					else
					{
						poco.JobDescriptions = null;
					}
					poco.TimeStamp = (byte[])reader[4];
					pocos[position] = poco;
					position++;

				}
				conn.Close();

			}
			return pocos.Where(a => a != null).ToList();
		}

		public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
		{
			throw new NotImplementedException();
		}

		public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
		{
			IQueryable<CompanyJobDescriptionPoco> pocos = GetAll().AsQueryable();
			return pocos.Where(where).FirstOrDefault();
		}

		public void Remove(params CompanyJobDescriptionPoco[] items)
		{
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				foreach (CompanyJobDescriptionPoco poco in items)
				{
					cmd.CommandText = @"DELETE from Company_Jobs_Descriptions Where Id=@Id";
					cmd.Parameters.AddWithValue("@Id", poco.Id);
					conn.Open();
					int numofrows = cmd.ExecuteNonQuery();
					conn.Close();
				}
			}
		}

		public void Update(params CompanyJobDescriptionPoco[] items)
		{
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				foreach (CompanyJobDescriptionPoco poco in items)
				{
					cmd.CommandText = @"UPDATE Company_Jobs_Descriptions
                                      SET Job=@Job,
											Job_Name=@Job_Name,
											Job_Descriptions=@Job_Descriptions                                            
											WHERE ID=@Id";
					cmd.Parameters.AddWithValue("@Id", poco.Id);
					cmd.Parameters.AddWithValue("@Job", poco.Job);
					cmd.Parameters.AddWithValue("@Job_Name", poco.JobName);
					cmd.Parameters.AddWithValue("@Job_Descriptions", poco.JobDescriptions);
					conn.Open();
					int numofrows = cmd.ExecuteNonQuery();
					conn.Close();
				}
			}
		}
	}
}
