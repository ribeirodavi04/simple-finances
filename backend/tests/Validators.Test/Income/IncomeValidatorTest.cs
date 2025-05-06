using Common.TestUtilities.Requests;
using SimpleFinances.Communication.Requests;
using SimpleFinances.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.Test.Income
{
    public class IncomeValidatorTest
    {
        [Fact]
        public void Success()
        {
            //Arrange
            var validator = new SimpleFinances.Application.UseCases.Income.Validator.IncomeValidator();
            var request = RequestIncomeJsonBuilder.Build();

            //Act
            var result = validator.Validate(request);

            //Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Error_Income_Type_Empty()
        {
            //Arrange
            var validator = new SimpleFinances.Application.UseCases.Income.Validator.IncomeValidator();
            var request = RequestIncomeJsonBuilder.Build();
            request.TypeName = "";

            //Act
            var result = validator.Validate(request);

            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, e => e.ErrorMessage == ResourceMessagesException.INCOME_TYPE_NAME_EMPTY);
        }
  
    }
}
