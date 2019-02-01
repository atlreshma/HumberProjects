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
	public class CompanyJobEducationRepository : BaseADO,IDataRepository<CompanyJobEducationPoco>
	{
		public void Add(params CompanyJobEducationPoco[] items)
		{
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand command = new SqlCommand();
				command.Connection = conn;
				foreach (CompanyJobEducationPoco poco in items)
				{
					command.CommandText = @"Insert into [dbo].[Company_Job_Educations]
                     ([Id],[Job],[Major],[Importance])
						Values (@Id,@Job,@Major,@Importance)";

					command.Parameters.AddWithValue("@Id", poco.Id);
					command.Parameters.AddWithValue("@Job", poco.Job);
					command.Parameters.AddWithValue("@Major", poco.Major);
					command.Parameters.AddWithValue("@Importance", poco.Importance);
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

		public IList<CompanyJobEducationPoco> GetAll(params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
		{
			CompanyJobEducationPoco[] pocos = new CompanyJobEducationPoco[1001];
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand command = new SqlCommand("Select * from Company_Job_Educations", conn);

				int position = 0;
				conn.Open();
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					CompanyJobEducationPoco poco = new CompanyJobEducationPoco();
					poco.Id = reader.GetGuid(0);
					poco.Job = reader.GetGuid(1);
					poco.Major = reader.GetString(2);
					poco.Importance = reader.GetInt16(3);
					poco.TimeStamp = (byte[])reader[4];
					pocos[position] = poco;
					position++;

				}
				conn.Close();

			}
			return pocos.Where(a => a != null).ToList();
		}

		public IList<CompanyJobEducationPoco> GetList(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
		{
			throw new NotImplementedException();
		}

		public CompanyJobEducationPoco GetSingle(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
		{
			IQueryable<CompanyJobEducationPoco> pocos = GetAll().AsQueryable();
			return pocos.Where(where).FirstOrDefault();
		}

		public void Remove(params CompanyJobEducationPoco[] items)
		{
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				foreach (CompanyJobEducationPoco poco in items)
				{
					cmd.CommandText = @"DELETE from Company_Job_Educations Where Id=@Id";
					cmd.Parameters.AddWithValue("@Id", poco.Id);
					conn.Open();
					int numofrows = cmd.ExecuteNonQuery();
					conn.Close();
				}
			}
		}

		public void Update(params CompanyJobEducationPoco[] items)
		{
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				foreach (CompanyJobEducationPoco poco in items)
				{
					cmd.CommandText = @"UPDATE Company_Job_Educations
                                      SET Job=@Job,
											Major=@Major,
											Importance=@Importance
											WHERE ID=@Id";
					cmd.Parameters.AddWithValue("@Id", poco.Id);
					cmd.Parameters.AddWithValue("@Job", poco.Job);
					cmd.Parameters.AddWithValue("@Major", poco.Major);
					cmd.Parameters.AddWithValue("@Importance", poco.Importance);
					conn.Open();
					int numofrows = cmd.ExecuteNonQuery();
					conn.Close();
				}
			}
		}
	}
}
