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
    public class CardUpdateOnlyRepositoryBuilder
    {
        private readonly Mock<ICardUpdateOnlyRepository> _repository;

        public CardUpdateOnlyRepositoryBuilder()
        {
            _repository = new Mock<ICardUpdateOnlyRepository>();    
        }

        public CardUpdateOnlyRepositoryBuilder GetCardById(int idCard, User user, SimpleFinances.Domain.Entities.Card card)
        {
            _repository.Setup(repository => repository.GetCardById(user, idCard)).ReturnsAsync(card);   
            return this;
        }

        public ICardUpdateOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
