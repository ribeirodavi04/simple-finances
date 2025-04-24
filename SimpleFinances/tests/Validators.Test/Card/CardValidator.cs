using Common.TestUtilities.Requests;
using FluentAssertions;
using SimpleFinances.Application.UseCases.Card.Validator;
using SimpleFinances.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.Test.Card
{
    public class CardValidator
    {
        [Fact]
        public void Success()
        {
            var validator = new SimpleFinances.Application.UseCases.Card.Validator.CardValidator();

            //Arrange 
            var request = RequestCardJsonBuilder.Build();

            //Act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_Name_Empty()
        {
            var validator = new SimpleFinances.Application.UseCases.Card.Validator.CardValidator();

            //Arrange
            var request = RequestCardJsonBuilder.Build();
            request.Name = string.Empty;

            //Act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.CARD_NAME_EMPTY));
        }

        [Fact]
        public void Error_Type_Name_Empty()
        {
            var validator = new SimpleFinances.Application.UseCases.Card.Validator.CardValidator();

            //Arrange
            var request = RequestCardJsonBuilder.Build();
            request.TypeName = string.Empty;

            //Act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.CARD_TYPE_EMPTY));
        }
    }
}
