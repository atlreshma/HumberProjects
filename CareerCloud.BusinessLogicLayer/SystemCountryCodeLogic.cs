using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
	public class SystemCountryCodeLogic
	{
		protected IDataRepository<SystemCountryCodePoco> _repository;

		public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> repository)
		{
			_repository = repository;
		}

		public void Verify(SystemCountryCodePoco[] pocos)
		{
			List<ValidationException> exceptions = new List<ValidationException>();
			foreach (var items in pocos)
			{
				if (string.IsNullOrEmpty(items.Code))
				{
					exceptions.Add(new ValidationException(900, $"{items.Code}"));
				}

				if (string.IsNullOrEmpty(items.Name))
				{
					exceptions.Add(new ValidationException(901, $"{items.Code}"));
				}
			}
			if (exceptions.Count > 0)
			{
				throw new AggregateException(exceptions);
			}
		}
		public SystemCountryCodePoco Get(string id)
		{
			return _repository.GetSingle(c => c.Code == id);
		}
		public List<SystemCountryCodePoco> GetAll()
		{
			return _repository.GetAll().ToList();
		}
		public void Add(SystemCountryCodePoco[] pocos)
		{
			Verify(pocos);
			_repository.Add(pocos);
		}
		public void Update(SystemCountryCodePoco[] pocos)
		{
			Verify(pocos);
			_repository.Update(pocos);
		}
		public void Delete(SystemCountryCodePoco[] pocos)
		{
			_repository.Remove(pocos);
		}
	}
}
