using Common.TestUtilities.Cryptography;
using Common.TestUtilities.Entities;
using Common.TestUtilities.LoggedUser;
using Common.TestUtilities.Mapper;
using Common.TestUtilities.Repositories;
using Common.TestUtilities.Repositories.Card;
using Common.TestUtilities.Requests;
using SimpleFinances.Application.UseCases.Card.Register;
using SimpleFinances.Domain.Entities;
using SimpleFinances.Exceptions;
using SimpleFinances.Exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Teste.Card.Register
{
    public class RegisterCardUseCaseTest
    {

        private RegisterCardUseCase CreateUseCase(User user, string? cardName = null)
        {
            var mapper = MapperBuilder.Build();
            //var passwordEncripter = PasswordEncripterBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var unityOfWork = UnityOfWorkBuilder.Build();
            var cardWriteOnlyRepository = CardWriteOnlyRepositoryBuilder.Build();
            var cardReadOnlyRepository = new CardReadOnlyRepositoryBuilder();

            if (cardName != null)
                cardReadOnlyRepository.ExistCardWithName(cardName, user.UserId);

            return new RegisterCardUseCase(cardWriteOnlyRepository, cardReadOnlyRepository.Build(), mapper, unityOfWork, loggedUser);
        }

        [Fact]
        public async Task Success()
        {
            //Arrange
            var request = RequestCardJsonBuilder.Build();
            var user = new UserBuilder().Build();

            var useCase = CreateUseCase(user);

            //Act
            var result = await useCase.Execute(request);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(request.Name, result.Name);
            Assert.Equal(request.Bank, result.Bank);
            Assert.Equal(request.TypeName, result.TypeName);
            Assert.Equal(request.Limit, result.Limit);
        }

        [Fact]
        public async Task Error_Card_Name_Empty()
        {
            //Arrange
            var request = RequestCardJsonBuilder.Build();
            request.Name = string.Empty;
            
            var user = new UserBuilder().Build();

            var useCase = CreateUseCase(user);

            //Act
            Func<Task> act = async () => await useCase.Execute(request);

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

            var user = new UserBuilder().Build();

            var useCase = CreateUseCase(user);

            //Act
            Func<Task> act = async () => await useCase.Execute(request);

            //Assert
            var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(act);
            Assert.Contains(ResourceMessagesException.CARD_TYPE_EMPTY, exception.ErrorsMessages);
        }

        [Fact]
        public async Task Error_Card_Name_Already_Registered()
        {
            //Arrange
            var request = RequestCardJsonBuilder.Build();
            var user = new UserBuilder().Build();
            var useCase = CreateUseCase(user, request.Name);

            //Act
            Func<Task> act = async () => await useCase.Execute(request);

            //Assert
            var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(act);
            Assert.Contains(ResourceMessagesException.CARD_NAME_ALREADY_EXIST, exception.ErrorsMessages);
        }
    }
}
