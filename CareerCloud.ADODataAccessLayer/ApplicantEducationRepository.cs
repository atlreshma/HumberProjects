using CareerCloud.Pocos;
using CareerCloud.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.SqlClient;

namespace CareerCloud.ADODataAccessLayer
{
	public class ApplicantEducationRepository : BaseADO, IDataRepository<ApplicantEducationPoco>
	{
		public void Add(params ApplicantEducationPoco[] items)
		{
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand command = new SqlCommand();
				command.Connection = conn;
				foreach (ApplicantEducationPoco poco in items)
				{
					command.CommandText = @"Insert into [dbo].[Applicant_Educations]
                     ([Id],[Applicant],[Major],[Certificate_Diploma],[Start_Date],[Completion_Date],[Completion_Percent])
						Values (@Id,@Applicant,@Major,@Certificate_Diploma,@Start_Date,@Completion_Date,@Completion_Percent)";

					command.Parameters.AddWithValue("@Id", poco.Id);
					command.Parameters.AddWithValue("@Applicant", poco.Applicant);
					command.Parameters.AddWithValue("@Major", poco.Major);
					command.Parameters.AddWithValue("@Certificate_Diploma", poco.CertificateDiploma);
					command.Parameters.AddWithValue("@Start_Date", poco.StartDate);
					command.Parameters.AddWithValue("@Completion_Date", poco.CompletionDate);
					command.Parameters.AddWithValue("@Completion_Percent", poco.CompletionPercent);
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

		public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
		{
			ApplicantEducationPoco[] pocos = new ApplicantEducationPoco[500];
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand command = new SqlCommand("Select * from Applicant_Educations", conn);

				int position = 0;
				conn.Open();
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					ApplicantEducationPoco poco = new ApplicantEducationPoco();
					poco.Id = reader.GetGuid(0);
					poco.Applicant = reader.GetGuid(1);
					poco.Major = reader.GetString(2);
					poco.CertificateDiploma = reader.GetString(3);
					if(!reader.IsDBNull(4))
					{
						poco.StartDate = reader.GetDateTime(4);
					}
					else
					{
						poco.StartDate = null;
					}
					if (!reader.IsDBNull(5))
					{
						poco.CompletionDate = reader.GetDateTime(5);
					}
					else
					{
						poco.CompletionDate = null;
					}
					if (!reader.IsDBNull(6))
					{
						poco.CompletionPercent = reader.GetByte(6);
					}
					else
					{
						poco.CompletionPercent = null;
					}
					poco.TimeStamp =(byte[])reader[7];
					pocos[position] = poco;
					position++;

				}
				conn.Close();
				
			}
			return pocos.Where(a=>a!=null).ToList();

		}

		public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
		{
			throw new NotImplementedException();
		}

		public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
		{
			IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();
			return pocos.Where(where).FirstOrDefault();

		}

		public void Remove(params ApplicantEducationPoco[] items)
		{
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				foreach (ApplicantEducationPoco poco in items)
				{
					cmd.CommandText = @"DELETE from Applicant_Educations Where Id=@Id";
					cmd.Parameters.AddWithValue("@Id", poco.Id);
					conn.Open();
					int numofrows = cmd.ExecuteNonQuery();
					conn.Close();
				}
			}


		}

		public void Update(params ApplicantEducationPoco[] items)
		{
			using (SqlConnection conn = new SqlConnection(connString))
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				foreach(ApplicantEducationPoco poco in items)
				{
					cmd.CommandText = @"UPDATE Applicant_Educations
                                      SET Applicant=@Applicant,
											Major=@Major,
											Certificate_Diploma=@Certificate_Diploma,
											Start_Date=@Start_Date,                                            
											Completion_Date=@Completion_Date,
											Completion_Percent=@Completion_Percent
											WHERE ID=@Id";
					cmd.Parameters.AddWithValue("@Id", poco.Id);
					cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
					cmd.Parameters.AddWithValue("@Major", poco.Major);
					cmd.Parameters.AddWithValue("@Certificate_Diploma", poco.CertificateDiploma);
					cmd.Parameters.AddWithValue("@Start_Date", poco.StartDate);
					cmd.Parameters.AddWithValue("@Completion_Date", poco.CompletionDate);
					cmd.Parameters.AddWithValue("@Completion_Percent", poco.CompletionPercent);
					conn.Open();
					int numofrows=cmd.ExecuteNonQuery();
					conn.Close();
				}
			}
		}
	}
}
