using Moq;
using SimpleFinances.Domain.Repositories.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.TestUtilities.Repositories.Card
{
    public class CardWriteOnlyRepositoryBuilder
    {
        public static ICardWriteOnlyRepository Build()
        {
            var mock = new Mock<ICardWriteOnlyRepository>();

            return mock.Object;
        }
    }
}
