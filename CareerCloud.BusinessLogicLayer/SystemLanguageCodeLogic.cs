using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
	public class SystemLanguageCodeLogic
	{
		protected IDataRepository<SystemLanguageCodePoco> _repository;

		public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository)
		{
			_repository = repository;
		}
		protected void Verify(SystemLanguageCodePoco[] pocos)
		{
			List<ValidationException> exceptions = new List<ValidationException>();

			foreach (var items in pocos)
			{
				if (string.IsNullOrEmpty(items.LanguageID))
				{
					exceptions.Add(new ValidationException(1000, $"{items.LanguageID}"));
				}
				if (string.IsNullOrEmpty(items.Name))
				{
					exceptions.Add(new ValidationException(1001, $"{items.LanguageID}"));
				}
				if (string.IsNullOrEmpty(items.NativeName))
				{
					exceptions.Add(new ValidationException(1002, $"{items.LanguageID}"));
				}
			}
			if (exceptions.Count > 0)
			{
				throw new AggregateException(exceptions);
			}
		}
		public SystemLanguageCodePoco Get(string id)
		{
			return _repository.GetSingle(c => c.LanguageID == id);
		}
		public List<SystemLanguageCodePoco> GetAll()
		{
			return _repository.GetAll().ToList();
		}
		public void Add(SystemLanguageCodePoco[] pocos)
		{
			Verify(pocos);
			_repository.Add(pocos);
		}
		public void Update(SystemLanguageCodePoco[] pocos)
		{
			Verify(pocos);
			_repository.Update(pocos);
		}
		public void Delete(SystemLanguageCodePoco[] pocos)
		{
			_repository.Remove(pocos);
		}
	}
}
