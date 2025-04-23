using Moq;
using SimpleFinances.Domain.Entities;
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

        public void ExistCardWithName(string cardName, int userId)
        {
            _repository.Setup(repository => repository.ExistCardName(cardName, userId)).ReturnsAsync(true);
        }

        public ICardReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
