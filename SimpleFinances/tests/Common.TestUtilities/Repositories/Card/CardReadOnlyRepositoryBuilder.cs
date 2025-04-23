using Moq;
using SimpleFinances.Domain.Repositories.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.TestUtilities.Repositories.Card
{
    public class CardReadOnlyRepositoryBuilder
    {
        private readonly Mock<ICardReadOnlyRepository> _repository;

        public CardReadOnlyRepositoryBuilder()
        {
            _repository = new Mock<ICardReadOnlyRepository>();
        }

        public ICardReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
