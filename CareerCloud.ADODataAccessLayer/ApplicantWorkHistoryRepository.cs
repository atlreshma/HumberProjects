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
	public class ApplicantWorkHistoryRepository : BaseADO,IDataRepository<ApplicantWorkHistoryPoco>
	{
		public void Add(params ApplicantWorkHistoryPoco[] items)
		{
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand command = new SqlCommand();
				command.Connection = conn;
				foreach (ApplicantWorkHistoryPoco poco in items)
				{
					command.CommandText = @"Insert into [dbo].[Applicant_Work_History]
                     ([Id],[Applicant],[Company_Name],[Country_Code],[Location],[Job_Title],[Job_Description],
						[Start_Month],[Start_Year],[End_Month],[End_Year])
						Values (@Id,@Applicant,@Company,@Country,@Loc,@JobTitle,@JobDes,
								@StartMonth,@StartYear,@EndMonth,@EndYear)";

					command.Parameters.AddWithValue("@Id", poco.Id);
					command.Parameters.AddWithValue("@Applicant", poco.Applicant);
					command.Parameters.AddWithValue("@Company", poco.CompanyName);
					command.Parameters.AddWithValue("@Country", poco.CountryCode);
					command.Parameters.AddWithValue("@Loc", poco.Location);
					command.Parameters.AddWithValue("@JobTitle", poco.JobTitle);
					command.Parameters.AddWithValue("@JobDes", poco.JobDescription);
					command.Parameters.AddWithValue("@StartMonth", poco.StartMonth);
					command.Parameters.AddWithValue("@StartYear", poco.StartYear);
					command.Parameters.AddWithValue("@EndMonth", poco.EndMonth);
					command.Parameters.AddWithValue("@EndYear", poco.EndYear);
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

		public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
		{
			ApplicantWorkHistoryPoco[] pocos = new ApplicantWorkHistoryPoco[500];
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand command = new SqlCommand("Select * from Applicant_Work_History", conn);

				int position = 0;
				conn.Open();
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco();
					poco.Id = reader.GetGuid(0);
					poco.Applicant = reader.GetGuid(1);
					poco.CompanyName = reader.GetString(2);
					poco.CountryCode = reader.GetString(3);
					poco.Location = reader.GetString(4);
					poco.JobTitle = reader.GetString(5);
					poco.JobDescription = reader.GetString(6);
					poco.StartMonth = reader.GetInt16(7);
					poco.StartYear = reader.GetInt32(8);
					poco.EndMonth = reader.GetInt16(9);
					poco.EndYear = reader.GetInt32(10);
					poco.TimeStamp = (byte[])reader[11];
					pocos[position] = poco;
					position++;

				}
				conn.Close();

			}
			return pocos.Where(a => a != null).ToList();
		}

		public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
		{
			throw new NotImplementedException();
		}

		public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
		{
			IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();
			return pocos.Where(where).FirstOrDefault();
		}

		public void Remove(params ApplicantWorkHistoryPoco[] items)
		{
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				foreach (ApplicantWorkHistoryPoco poco in items)
				{
					cmd.CommandText = @"DELETE from Applicant_Work_History Where Id=@Id";
					cmd.Parameters.AddWithValue("@Id", poco.Id);
					conn.Open();
					int numofrows = cmd.ExecuteNonQuery();
					conn.Close();
				}
			}
		}

		public void Update(params ApplicantWorkHistoryPoco[] items)
		{
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				foreach (ApplicantWorkHistoryPoco poco in items)
				{
					cmd.CommandText = @"UPDATE Applicant_Work_History
                                      SET Applicant=@Applicant,
											Company_Name=@Company,
											Country_Code=@Country,
											Location=@Loc,                                            
											Job_Title=@JobTitle,
											Job_Description=@JobDes,
											Start_Month=@StartMonth,
											Start_Year=@StartYear,
											End_Month=@EndMonth,
											End_Year=@EndYear
											WHERE ID=@Id";
					cmd.Parameters.AddWithValue("@Id", poco.Id);
					cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
					cmd.Parameters.AddWithValue("@Company", poco.CompanyName);
					cmd.Parameters.AddWithValue("@Country", poco.CountryCode);
					cmd.Parameters.AddWithValue("@Loc", poco.Location);
					cmd.Parameters.AddWithValue("@JobTitle", poco.JobTitle);
					cmd.Parameters.AddWithValue("@JobDes", poco.JobDescription);
					cmd.Parameters.AddWithValue("@StartMonth", poco.StartMonth);
					cmd.Parameters.AddWithValue("@StartYear", poco.StartYear);
					cmd.Parameters.AddWithValue("@EndMonth", poco.EndMonth);
					cmd.Parameters.AddWithValue("@EndYear", poco.EndYear);
					conn.Open();
					int numofrows = cmd.ExecuteNonQuery();
					conn.Close();
				}
			}
		}
	}
}
