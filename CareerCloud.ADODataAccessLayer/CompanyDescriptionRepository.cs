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
	public class CompanyDescriptionRepository : BaseADO,IDataRepository<CompanyDescriptionPoco>
	{
		public void Add(params CompanyDescriptionPoco[] items)
		{
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand command = new SqlCommand();
				command.Connection = conn;
				foreach (CompanyDescriptionPoco poco in items)
				{
					command.CommandText = @"Insert into [dbo].[Company_Descriptions]
                     ([Id],[Company],[LanguageID],[Company_Name],[Company_Description])
						Values (@Id,@Company,@LanguageID,@Company_Name,@Company_Description)";

					command.Parameters.AddWithValue("@Id", poco.Id);
					command.Parameters.AddWithValue("@Company", poco.Company);
					command.Parameters.AddWithValue("@LanguageID", poco.LanguageId);
					command.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
					command.Parameters.AddWithValue("@Company_Description", poco.CompanyDescription);
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

		public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
		{
			CompanyDescriptionPoco[] pocos = new CompanyDescriptionPoco[650];
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand command = new SqlCommand("Select * from Company_Descriptions", conn);

				int position = 0;
				conn.Open();
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					CompanyDescriptionPoco poco = new CompanyDescriptionPoco();
					poco.Id = reader.GetGuid(0);
					poco.Company = reader.GetGuid(1);
					poco.LanguageId = reader.GetString(2);
					poco.CompanyName = reader.GetString(3);
					poco.CompanyDescription = reader.GetString(4);
					poco.TimeStamp = (byte[])reader[5];
					pocos[position] = poco;
					position++;

				}
				conn.Close();

			}
			return pocos.Where(a => a != null).ToList();
		}

		public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
		{
			throw new NotImplementedException();
		}

		public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
		{
			IQueryable<CompanyDescriptionPoco> pocos = GetAll().AsQueryable();
			return pocos.Where(where).FirstOrDefault();
		}

		public void Remove(params CompanyDescriptionPoco[] items)
		{
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				foreach (CompanyDescriptionPoco poco in items)
				{
					cmd.CommandText = @"DELETE from Company_Descriptions Where Id=@Id";
					cmd.Parameters.AddWithValue("@Id", poco.Id);
					conn.Open();
					int numofrows = cmd.ExecuteNonQuery();
					conn.Close();
				}
			}
		}

		public void Update(params CompanyDescriptionPoco[] items)
		{
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				foreach (CompanyDescriptionPoco poco in items)
				{
					cmd.CommandText = @"UPDATE Company_Descriptions
                                      SET Company=@Company,
										    LanguageID=@LanguageID,
											Company_Name=@Company_Name,
											Company_Description=@Company_Description                                            
											WHERE ID=@Id";
					cmd.Parameters.AddWithValue("@Id", poco.Id);
					cmd.Parameters.AddWithValue("@Company", poco.Company);
					cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageId);
					cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
					cmd.Parameters.AddWithValue("@Company_Description", poco.CompanyDescription);
					conn.Open();
					int numofrows = cmd.ExecuteNonQuery();
					conn.Close();
				}
			}
		}
	}
}
