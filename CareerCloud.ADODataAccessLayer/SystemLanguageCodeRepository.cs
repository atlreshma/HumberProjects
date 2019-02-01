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
	public class SystemLanguageCodeRepository : BaseADO,IDataRepository<SystemLanguageCodePoco>
	{
		public void Add(params SystemLanguageCodePoco[] items)
		{
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand command = new SqlCommand();
				command.Connection = conn;
				foreach (SystemLanguageCodePoco poco in items)
				{
					command.CommandText = @"Insert into [dbo].[System_Language_Codes]
                     ([LanguageID],[Name],[Native_Name])
						Values (@LanguageID,@Name,@NativeName)";

					command.Parameters.AddWithValue("@LanguageID", poco.LanguageID);
					command.Parameters.AddWithValue("@Name", poco.Name);
					command.Parameters.AddWithValue("@NativeName", poco.NativeName);
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

		public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
		{
			SystemLanguageCodePoco[] pocos = new SystemLanguageCodePoco[500];
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand command = new SqlCommand("Select * from System_Language_Codes", conn);

				int position = 0;
				conn.Open();
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					SystemLanguageCodePoco poco = new SystemLanguageCodePoco();
					poco.LanguageID = reader.GetString(0);
					poco.Name = reader.GetString(1);
					poco.NativeName = reader.GetString(2);
					pocos[position] = poco;
					position++;

				}
				conn.Close();

			}
			return pocos.Where(a => a != null).ToList();
		}

		public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
		{
			throw new NotImplementedException();
		}

		public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
		{
			IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();
			return pocos.Where(where).FirstOrDefault();
		}

		public void Remove(params SystemLanguageCodePoco[] items)
		{
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				foreach (SystemLanguageCodePoco poco in items)
				{
					cmd.CommandText = @"DELETE from System_Language_Codes Where LanguageID=@LanguageID";
					cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageID);
					conn.Open();
					int numofrows = cmd.ExecuteNonQuery();
					conn.Close();
				}
			}
		}

		public void Update(params SystemLanguageCodePoco[] items)
		{

			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				foreach (SystemLanguageCodePoco poco in items)
				{
					cmd.CommandText = @"UPDATE System_Language_Codes
                                      SET Name=@Name,
										  Native_Name=@NativeName
											WHERE LanguageID=@LanguageID";
					cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageID);
					cmd.Parameters.AddWithValue("@Name", poco.Name);
					cmd.Parameters.AddWithValue("@NativeName", poco.NativeName);
					conn.Open();
					int numofrows = cmd.ExecuteNonQuery();
					conn.Close();
				}
			}

		}
	}
}
