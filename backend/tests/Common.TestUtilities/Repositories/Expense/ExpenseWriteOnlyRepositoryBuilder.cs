using Moq;
using SimpleFinances.Domain.Repositories.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.TestUtilities.Repositories.Expense
{
    public class ExpenseWriteOnlyRepositoryBuilder
    {
        public static IExpenseWriteOnlyRepository Build()
        {
            var mock = new Mock<IExpenseWriteOnlyRepository>();
            return mock.Object;
        }
    }
}
