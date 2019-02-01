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
	public class SystemCountryCodeRepository : BaseADO,IDataRepository<SystemCountryCodePoco>
	{
		public void Add(params SystemCountryCodePoco[] items)
		{
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand command = new SqlCommand();
				command.Connection = conn;
				foreach (SystemCountryCodePoco poco in items)
				{
					command.CommandText = @"Insert into [dbo].[System_Country_Codes]
                     ([Code],[Name])
						Values (@Code,@Name)";

					command.Parameters.AddWithValue("@Code", poco.Code);
					command.Parameters.AddWithValue("@Name", poco.Name);
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

		public IList<SystemCountryCodePoco> GetAll(params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
		{
			SystemCountryCodePoco[] pocos = new SystemCountryCodePoco[500];
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand command = new SqlCommand("Select * from System_Country_Codes", conn);

				int position = 0;
				conn.Open();
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					SystemCountryCodePoco poco = new SystemCountryCodePoco();
					poco.Code = reader.GetString(0);
					poco.Name = reader.GetString(1);
					pocos[position] = poco;
					position++;

				}
				conn.Close();

			}
			return pocos.Where(a => a != null).ToList();
		}

		public IList<SystemCountryCodePoco> GetList(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
		{
			throw new NotImplementedException();
		}

		public SystemCountryCodePoco GetSingle(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
		{
			IQueryable<SystemCountryCodePoco> pocos = GetAll().AsQueryable();
			return pocos.Where(where).FirstOrDefault();
		}

		public void Remove(params SystemCountryCodePoco[] items)
		{
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				foreach (SystemCountryCodePoco poco in items)
				{
					cmd.CommandText = @"DELETE from System_Country_Codes Where Code=@Code";
					cmd.Parameters.AddWithValue("@Code", poco.Code);
					conn.Open();
					int numofrows = cmd.ExecuteNonQuery();
					conn.Close();
				}
			}
		}

		public void Update(params SystemCountryCodePoco[] items)
		{
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				foreach (SystemCountryCodePoco poco in items)
				{
					cmd.CommandText = @"UPDATE System_Country_Codes
                                      SET Name=@Name
											WHERE Code=@Code";
					cmd.Parameters.AddWithValue("@Code", poco.Code);
					cmd.Parameters.AddWithValue("@Name", poco.Name);
					conn.Open();
					int numofrows = cmd.ExecuteNonQuery();
					conn.Close();
				}
			}
		}
	}
}
