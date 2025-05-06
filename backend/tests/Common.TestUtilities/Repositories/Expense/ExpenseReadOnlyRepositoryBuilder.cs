using Moq;
using SimpleFinances.Domain.Entities;
using SimpleFinances.Domain.Repositories.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.TestUtilities.Repositories.Expense
{
    public class ExpenseReadOnlyRepositoryBuilder
    {
        private readonly Mock<IExpenseReadOnlyRepository> _repository;

        public ExpenseReadOnlyRepositoryBuilder()
        {
            _repository = new Mock<IExpenseReadOnlyRepository>();
        }

        public ExpenseReadOnlyRepositoryBuilder GetExpenseById(User user, SimpleFinances.Domain.Entities.Expense? expense)
        {
            if(expense is not null)
                _repository.Setup(repository => repository.GetExpenseById(user, expense.ExpenseId)).ReturnsAsync(expense);

            return this;
        }

        public IExpenseReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
