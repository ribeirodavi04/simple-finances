using Common.TestUtilities.Entities;
using Common.TestUtilities.LoggedUser;
using Common.TestUtilities.Mapper;
using Common.TestUtilities.Repositories;
using Common.TestUtilities.Repositories.Income;
using Common.TestUtilities.Requests;
using SimpleFinances.Application.UseCases.Income.Register;
using SimpleFinances.Domain.Entities;
using SimpleFinances.Exceptions.ExceptionsBase;
using SimpleFinances.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Test.Income.Register
{
    public class RegisterIncomeUseCaseTest
    {
        public RegisterIncomeUseCase CreateUseCase(User user, string? incomeTypeName = null)
        {
            var mapper = MapperBuilder.Build();
            var unityOfWork = UnityOfWorkBuilder.Build();
            var writeRepository = IncomeWriteOnlyRepositoryBuilder.Build();
            var readRepository = new IncomeReadOnlyRepositoryBuilder();
            var loggedUser = LoggedUserBuilder.Build(user);

            if (incomeTypeName != null)
                readRepository.ExistIncomeTypeName(incomeTypeName, user.UserId);

            return new RegisterIncomeUseCase(writeRepository, readRepository.Build(), mapper, unityOfWork, loggedUser);
        }

        [Fact]
        public async Task Success()
        {
            //Arrange
            var request = RequestIncomeJsonBuilder.Build();
            var user = new UserBuilder().Build();

            var useCase = CreateUseCase(user);

            //Act
            var result = await useCase.Execute(request);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(request.TypeName, result.TypeName);
            Assert.Equal(request.Amount, result.Amount);
            Assert.Equal(request.DateReceived, result.DateReceived);
            Assert.Equal(request.IsRecurring, result.IsRecurring);
        }

        [Fact]
        public async Task Error_Income_Type_Name_Empty()
        {
            //Arrange
            var request = RequestIncomeJsonBuilder.Build();
            request.TypeName = string.Empty;

            var user = new UserBuilder().Build();

            var useCase = CreateUseCase(user);

            //Act
            Func<Task> act = async () => await useCase.Execute(request);

            //Assert
            var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(act);
            Assert.Contains(ResourceMessagesException.INCOME_TYPE_NAME_EMPTY, exception.ErrorsMessages);
        }
    }
}
