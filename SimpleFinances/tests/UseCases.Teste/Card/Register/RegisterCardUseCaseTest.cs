using Common.TestUtilities.Cryptography;
using Common.TestUtilities.Entities;
using Common.TestUtilities.LoggedUser;
using Common.TestUtilities.Mapper;
using Common.TestUtilities.Repositories;
using Common.TestUtilities.Repositories.Card;
using Common.TestUtilities.Requests;
using SimpleFinances.Application.UseCases.Card.Register;
using SimpleFinances.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Teste.Card.Register
{
    public class RegisterCardUseCaseTest
    {

        private RegisterCardUseCase CreateUseCase(User user)
        {
            var mapper = MapperBuilder.Build();
            //var passwordEncripter = PasswordEncripterBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var unityOfWork = UnityOfWorkBuilder.Build();
            var cardWriteOnlyRepository = CardWriteOnlyRepositoryBuilder.Build();
            var cardReadOnlyRepository = new CardReadOnlyRepositoryBuilder();

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
    }
}
