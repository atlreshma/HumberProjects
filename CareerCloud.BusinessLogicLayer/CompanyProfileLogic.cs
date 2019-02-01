using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
	public class CompanyProfileLogic:BaseLogic<CompanyProfilePoco>
	{
		public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository)
			: base(repository)
		{

		}
		public override void Add(CompanyProfilePoco[] pocos)
		{
			Verify(pocos);
			base.Add(pocos);
		}
		public override void Update(CompanyProfilePoco[] pocos)
		{
			Verify(pocos);
			base.Update(pocos);
		}
		protected override void Verify(CompanyProfilePoco[] pocos)
		{
			List<ValidationException> exceptions = new List<ValidationException>();

			foreach (var poco in pocos)
			{
				if (!string.IsNullOrEmpty(poco.CompanyWebsite)) {

					string[] requiredWebsiteExtensions = new string[] { "ca", "com", "biz" };
					string[] websiteComponents = poco.CompanyWebsite.Split('.');

					if (!requiredWebsiteExtensions.Any(t => websiteComponents[1].Contains(t)))
					{
						exceptions.Add(new ValidationException(600, $"CompanyWebsite {poco.Id} is not in a valid format"));
					}
				}

				if (!string.IsNullOrEmpty(poco.ContactPhone))
				{
					string[] phoneComponents = poco.ContactPhone.Split('-');
					if (phoneComponents.Length < 3)
					{
						exceptions.Add(new ValidationException(601, $"PhoneNumber {poco.Id} is not in the required format."));
					}
					else
					{
						if (phoneComponents[0].Length < 3)
						{
							exceptions.Add(new ValidationException(601, $"PhoneNumber {poco.Id} is not in the required format."));
						}
						else if (phoneComponents[1].Length < 3)
						{
							exceptions.Add(new ValidationException(601, $"PhoneNumber {poco.Id} is not in the required format."));
						}
						else if (phoneComponents[2].Length < 4)
						{
							exceptions.Add(new ValidationException(601, $"PhoneNumber {poco.Id} is not in the required format."));
						}
					}
				}
				else
				{
					exceptions.Add(new ValidationException(601, $"PhoneNumber {poco.Id} is not in the required format."));

				}


			}
			if (exceptions.Count > 0)
			{
				throw new AggregateException(exceptions);
			}
		}
	}
}
