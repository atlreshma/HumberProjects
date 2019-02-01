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
	public class SecurityLoginsLogRepository : BaseADO,IDataRepository<SecurityLoginsLogPoco>
	{
		public void Add(params SecurityLoginsLogPoco[] items)
		{
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand command = new SqlCommand();
				command.Connection = conn;
				foreach (SecurityLoginsLogPoco poco in items)
				{
					command.CommandText = @"Insert into [dbo].[Security_Logins_Log]
                     ([Id],[Login],[Source_IP],[Logon_Date],[Is_Succesful])
						Values (@Id,@Login,@IP,@LogonDate,@IsSuccesful)";

					command.Parameters.AddWithValue("@Id", poco.Id);
					command.Parameters.AddWithValue("@Login", poco.Login);
					command.Parameters.AddWithValue("@IP", poco.SourceIP);
					command.Parameters.AddWithValue("@LogonDate", poco.LogonDate);
					command.Parameters.AddWithValue("@IsSuccesful", poco.IsSuccesful);
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

		public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
		{
			SecurityLoginsLogPoco[] pocos = new SecurityLoginsLogPoco[1730];
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand command = new SqlCommand("Select * from Security_Logins_Log", conn);

				int position = 0;
				conn.Open();
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					SecurityLoginsLogPoco poco = new SecurityLoginsLogPoco();
					poco.Id = reader.GetGuid(0);
					poco.Login = reader.GetGuid(1);
					poco.SourceIP = reader.GetString(2);
					poco.LogonDate = reader.GetDateTime(3);
					poco.IsSuccesful = reader.GetBoolean(4);
					pocos[position] = poco;
					position++;

				}
				conn.Close();

			}
			return pocos.Where(a => a != null).ToList();
		}

		public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
		{
			throw new NotImplementedException();
		}

		public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
		{
			IQueryable<SecurityLoginsLogPoco> pocos = GetAll().AsQueryable();
			return pocos.Where(where).FirstOrDefault();
		}

		public void Remove(params SecurityLoginsLogPoco[] items)
		{
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				foreach (SecurityLoginsLogPoco poco in items)
				{
					cmd.CommandText = @"DELETE from Security_Logins_Log Where Id=@Id";
					cmd.Parameters.AddWithValue("@Id", poco.Id);
					conn.Open();
					int numofrows = cmd.ExecuteNonQuery();
					conn.Close();
				}
			}
		}

		public void Update(params SecurityLoginsLogPoco[] items)
		{
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				foreach (SecurityLoginsLogPoco poco in items)
				{
					cmd.CommandText = @"UPDATE Security_Logins_Log
                                      SET Login=@Login,
											Source_IP=@IP,
											Logon_Date=@LogonDate,
											Is_Succesful=@IsSuccesful                                            
											WHERE ID=@Id";
					cmd.Parameters.AddWithValue("@Id", poco.Id);
					cmd.Parameters.AddWithValue("@Login", poco.Login);
					cmd.Parameters.AddWithValue("@IP", poco.SourceIP);
					cmd.Parameters.AddWithValue("@LogonDate", poco.LogonDate);
					cmd.Parameters.AddWithValue("@IsSuccesful", poco.IsSuccesful);
					conn.Open();
					int numofrows = cmd.ExecuteNonQuery();
					conn.Close();
				}
			}
		}
	}
}
