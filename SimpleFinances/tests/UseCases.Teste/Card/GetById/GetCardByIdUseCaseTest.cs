using Common.TestUtilities.Entities;
using Common.TestUtilities.LoggedUser;
using Common.TestUtilities.Mapper;
using Common.TestUtilities.Repositories.Card;
using SimpleFinances.Application.UseCases.Card.GetById;
using SimpleFinances.Domain.Entities;
using SimpleFinances.Exceptions;
using SimpleFinances.Exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Test.Card.GetById
{
    public class GetCardByIdUseCaseTest
    {
        private static GetCardByIdUseCase CreateUseCase(User user, SimpleFinances.Domain.Entities.Card? card = null)
        {
            var loggedUser = LoggedUserBuilder.Build(user);
            var mapper = MapperBuilder.Build();
            var cardReadOnlyRepository = new CardReadOnlyRepositoryBuilder().GetCardById(user, card).Build();

            return new GetCardByIdUseCase(cardReadOnlyRepository, mapper, loggedUser);
        }

        [Fact]
        public async Task Success()
        {
            //Arrange
            var user = new UserBuilder().Build();   
            var card = new CardBuilder().Build();

            var useCase = CreateUseCase(user, card);

            //Act
            var result = await useCase.Execute(card.CardId);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(card.Name, result.Name);
            Assert.Equal(card.Bank, result.Bank);
            Assert.Equal(card.TypeName, result.TypeName);
            Assert.Equal(card.Limit, result.Limit);
        }

        [Fact]
        public async Task Error_Card_Not_Found()
        {
            //Arrange
            var user = new UserBuilder().Build();

            var useCase = CreateUseCase(user);

            //Act
            Func<Task> act = async () => await useCase.Execute(1);

            //Assert
            var exception = await Assert.ThrowsAsync<SimpleFinancesException>(act);
            Assert.Equal(ResourceMessagesException.CARD_NOT_FOUND, exception.Message);
        }
    }
}
