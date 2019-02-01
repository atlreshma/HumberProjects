using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
	public abstract class BaseADO
	{
		protected string connString;

		public BaseADO()
		{
			//connString = @"Data Source=LAPTOP-0AOE9SB2\HUMBERBRIDGING;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True;";
			connString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
		}
	}
}
