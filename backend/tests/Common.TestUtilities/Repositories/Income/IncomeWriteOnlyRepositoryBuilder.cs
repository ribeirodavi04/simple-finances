using Moq;
using SimpleFinances.Domain.Repositories.Income;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.TestUtilities.Repositories.Income
{
    public class IncomeWriteOnlyRepositoryBuilder
    {
        public static IIncomeWriteOnlyRepository Build()
        {
            var mock = new Mock<IIncomeWriteOnlyRepository>();
            return mock.Object; 
        }
    }
}
