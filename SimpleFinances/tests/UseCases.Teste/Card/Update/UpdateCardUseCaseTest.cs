using Common.TestUtilities.Entities;
using Common.TestUtilities.LoggedUser;
using Common.TestUtilities.Mapper;
using Common.TestUtilities.Repositories;
using Common.TestUtilities.Repositories.Card;
using Common.TestUtilities.Requests;
using SimpleFinances.Application.UseCases.Card.Update;
using SimpleFinances.Domain.Entities;
using SimpleFinances.Exceptions.ExceptionsBase;
using SimpleFinances.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Teste.Card.Update
{
    public class UpdateCardUseCaseTest
    {
        private UpdateCardUseCase CreateUseCase(User user, int idCard, SimpleFinances.Domain.Entities.Card card, bool cardFound = true, string? cardName = null)
        {
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var unityOfWork = UnityOfWorkBuilder.Build();
            var cardReadOnlyRepository = new CardReadOnlyRepositoryBuilder();
            var cardUpdateOnlyRepository = new CardUpdateOnlyRepositoryBuilder();


            if (cardName != null)
                cardReadOnlyRepository.ExistCardWithName(cardName, user.UserId);


            if (cardFound)
                cardUpdateOnlyRepository.GetCardById(idCard, user, card);
            else
                cardUpdateOnlyRepository.GetCardById(idCard, user, null!);

            return new UpdateCardUseCase(cardUpdateOnlyRepository.Build(), cardReadOnlyRepository.Build(), unityOfWork, mapper, loggedUser);
        }

        [Fact]
        public async Task Success()
        {
            //Arrange
            var request = RequestCardJsonBuilder.Build();
            var card = new CardBuilder().Build();
            var user = new UserBuilder().Build();
            card.UserId = user.UserId;

            var useCase = CreateUseCase(user, card.CardId, card);

            //Act
            Func<Task> act = async () => await useCase.Execute(card.CardId, request);
            await act();

            //Assertion
            Assert.Equal(request.Name, card.Name);
            Assert.Equal(request.Bank, card.Bank);
            Assert.Equal(request.TypeName, card.TypeName);
            Assert.Equal(request.Limit, card.Limit);
            Assert.Equal(request.CardDueDate, card.CardDueDate);
            Assert.Equal(request.CardClosingDate, card.CardClosingDate);
        }

        [Fact]
        public async Task Error_Card_Not_Found()
        {
            //Arrange
            var request = RequestCardJsonBuilder.Build();
            var card = new CardBuilder().Build();
            var user = new UserBuilder().Build();
            card.UserId = user.UserId;

            var useCase = CreateUseCase(user, card.CardId, card, cardFound: false);

            //Act
            Func<Task> act = async () => await useCase.Execute(card.CardId, request);

            //Assert
            var exception = await Assert.ThrowsAsync<SimpleFinancesException>(act);
            Assert.Equal(ResourceMessagesException.CARD_NOT_FOUND, exception.Message);
        }

        [Fact]
        public async Task Error_Card_Name_Empty()
        {
            //Arrange
            var request = RequestCardJsonBuilder.Build();
            request.Name = string.Empty;

            var card = new CardBuilder().Build();
            var user = new UserBuilder().Build();
            card.UserId = user.UserId;

            var useCase = CreateUseCase(user, card.CardId, card);

            //Act
            Func<Task> act = async () => await useCase.Execute(card.CardId, request);

            //Assert
            var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(act);
            Assert.Contains(ResourceMessagesException.CARD_NAME_EMPTY, exception.ErrorsMessages);
        }

        [Fact]
        public async Task Error_Card_Type_Empty()
        {
            //Arrange
            var request = RequestCardJsonBuilder.Build();
            request.TypeName = string.Empty;

            var card = new CardBuilder().Build();
            var user = new UserBuilder().Build();
            card.UserId = user.UserId;

            var useCase = CreateUseCase(user, card.CardId, card);

            //Act
            Func<Task> act = async () => await useCase.Execute(card.CardId, request);

            //Assert
            var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(act);
            Assert.Contains(ResourceMessagesException.CARD_TYPE_EMPTY, exception.ErrorsMessages);
        }

        [Fact]
        public async Task Error_Card_Name_Already_Registered()
        {
            //Arrange
            var request = RequestCardJsonBuilder.Build();
            var card = new CardBuilder().Build();
            var user = new UserBuilder().Build();
            card.UserId = user.UserId;

            var useCase = CreateUseCase(user, card.CardId, card, cardFound: true, request.Name);

            //Act
            Func<Task> act = async () => await useCase.Execute(card.CardId, request);

            //Assert
            var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(act);
            Assert.Contains(ResourceMessagesException.CARD_NAME_ALREADY_EXIST, exception.ErrorsMessages);
        }
    }
}
