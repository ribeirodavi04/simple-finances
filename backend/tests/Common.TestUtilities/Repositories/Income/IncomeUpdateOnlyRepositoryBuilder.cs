using Moq;
using SimpleFinances.Domain.Entities;
using SimpleFinances.Domain.Repositories.Income;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.TestUtilities.Repositories.Income
{
    public class IncomeUpdateOnlyRepositoryBuilder
    {
        private readonly Mock<IIncomeUpdateOnlyRepository> _repository;

        public IncomeUpdateOnlyRepositoryBuilder()
        {
            _repository = new Mock<IIncomeUpdateOnlyRepository>();
        }

        public IncomeUpdateOnlyRepositoryBuilder GetIncomeById(int idIncome, User user, SimpleFinances.Domain.Entities.Income income)
        {
            _repository.Setup(repository => repository.GetIncomeById(user, idIncome)).ReturnsAsync(income);
            return this;
        }

        public IIncomeUpdateOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
