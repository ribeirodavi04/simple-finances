using Moq;
using SimpleFinances.Domain.Entities;
using SimpleFinances.Domain.Repositories.Card;
using SimpleFinances.Domain.Repositories.Income;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.TestUtilities.Repositories.Income
{
    public class IncomeReadOnlyRepositoryBuilder
    {
        private readonly Mock<IIncomeReadOnlyRepository> _repository;

        public IncomeReadOnlyRepositoryBuilder()
        {
            _repository = new Mock<IIncomeReadOnlyRepository>();
        }

        public void ExistIncomeTypeName(string incomeTypeName, int userId)
        {
            _repository.Setup(repository => repository.ExistIncomeTypeName(incomeTypeName, userId)).ReturnsAsync(true);
        }

        public IncomeReadOnlyRepositoryBuilder GetIncomeById(User user, SimpleFinances.Domain.Entities.Income? income)
        {
            if (income is not null) 
                _repository.Setup(repository => repository.GetIncomeById(user, income.IncomeId)).ReturnsAsync(income); 

            return this;
        }

        public IncomeReadOnlyRepositoryBuilder GetAllIncomes(User user, IList<SimpleFinances.Domain.Entities.Income>? incomes)
        {
            if (incomes is not null)
                _repository.Setup(repository => repository.GetAllIncomes(user)).ReturnsAsync(incomes);

            return this;
        }

        public IIncomeReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
