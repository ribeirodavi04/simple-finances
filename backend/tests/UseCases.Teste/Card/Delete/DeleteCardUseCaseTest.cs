using Common.TestUtilities.Entities;
using Common.TestUtilities.LoggedUser;
using Common.TestUtilities.Repositories;
using Common.TestUtilities.Repositories.Card;
using SimpleFinances.Application.UseCases.Card.Delete;
using SimpleFinances.Domain.Entities;
using SimpleFinances.Exceptions;
using SimpleFinances.Exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Test.Card.Delete
{
    public class DeleteCardUseCaseTest
    {
        private static DeleteCardUseCase CreateUseCase(User user, SimpleFinances.Domain.Entities.Card? card = null)
        {
            var loggedUser = LoggedUserBuilder.Build(user);
            var cardReadOnlyRepository = new CardReadOnlyRepositoryBuilder().GetCardById(user, card).Build();
            var cardWriteOnlyRepository = CardWriteOnlyRepositoryBuilder.Build(); 
            var unityOfWork = UnityOfWorkBuilder.Build();

            return new DeleteCardUseCase(cardWriteOnlyRepository, cardReadOnlyRepository, unityOfWork, loggedUser);
        }

        [Fact]
        public async Task Success()
        {
            //Arrange
            var user = new UserBuilder().Build();
            var card = CardBuilder.Build(user);

            var useCase = CreateUseCase(user, card);

            //Act & Assertion
            Func<Task> act = async () => await useCase.Execute(card.CardId);
            await act();

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
            var expection = await Assert.ThrowsAsync<SimpleFinancesException>(act);
            Assert.Equal(ResourceMessagesException.CARD_NOT_FOUND, expection.Message);
        }
    }
}
