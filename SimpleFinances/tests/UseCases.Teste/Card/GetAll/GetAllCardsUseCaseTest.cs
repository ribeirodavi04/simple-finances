using Common.TestUtilities.Entities;
using Common.TestUtilities.LoggedUser;
using Common.TestUtilities.Mapper;
using Common.TestUtilities.Repositories.Card;
using SimpleFinances.Application.UseCases.Card.GetAll;
using SimpleFinances.Domain.Entities;
using SimpleFinances.Exceptions;
using SimpleFinances.Exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Test.Card.GetAll
{
    public class GetAllCardsUseCaseTest
    {
        private static GetAllCardsUseCase CreateUseCase(User user, IList<SimpleFinances.Domain.Entities.Card>? cards = null)
        {
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var cardReadOnlyRepository = new CardReadOnlyRepositoryBuilder().GetAllCards(user, cards).Build();

            return new GetAllCardsUseCase(cardReadOnlyRepository, mapper, loggedUser);
        }

        [Fact]
        public async Task Success()
        {
            //Arrange
            var user = new UserBuilder().Build();
            var cards = CardBuilder.Collection(user);

            var useCase = CreateUseCase(user, cards);

            //Act
            var result = await useCase.Execute();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(cards.Count, result.Count);
        }

        [Fact]
        public async Task Error_Card_Not_Found()
        {
            //Arrange
            var user = new UserBuilder().Build();

            var useCase = CreateUseCase(user, null);

            //Act
            Func<Task> act = async () => await useCase.Execute();

            //Assert
            var exception = await Assert.ThrowsAsync<SimpleFinancesException>(act);
            Assert.Equal(ResourceMessagesException.CARD_NOT_FOUND, exception.Message);
        }
    }
}
