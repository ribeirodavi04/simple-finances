using Common.TestUtilities.Requests;
using FluentAssertions;
using SimpleFinances.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.Test.Expense
{
    public class ExpenseValidatorTest
    {
        [Fact]
        public void Success()
        {
            //Arrange 
            var validator = new SimpleFinances.Application.UseCases.Expense.Validator.ExpenseValidator();
            var request = RequestExpenseJsonBuilder.Build();

            //Act
            var result = validator.Validate(request);

            //Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Error_Expense_Name_Empty()
        {
            var validator = new SimpleFinances.Application.UseCases.Expense.Validator.ExpenseValidator();

            //Arrange
            var request = RequestExpenseJsonBuilder.Build();
            request.Name = string.Empty;

            //Act
            var result = validator.Validate(request);

            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, e => e.ErrorMessage == ResourceMessagesException.EXPENSE_NAME_EMPTY);
        }

        [Fact]
        public void Error_Expense_Type_Name_Empty()
        {
            var validator = new SimpleFinances.Application.UseCases.Expense.Validator.ExpenseValidator();

            //Arrange
            var request = RequestExpenseJsonBuilder.Build();
            request.TypeName = string.Empty;

            //Act
            var result = validator.Validate(request);

            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, e => e.ErrorMessage == ResourceMessagesException.EXPENSE_TYPE_NAME_EMPTY);
        }

        [Fact]
        public void Error_Expense_Amount_Empty()
        {
            var validator = new SimpleFinances.Application.UseCases.Expense.Validator.ExpenseValidator();

            //Arrange
            var request = RequestExpenseJsonBuilder.Build();
            request.Amount = 0;

            //Act
            var result = validator.Validate(request);

            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, e => e.ErrorMessage == ResourceMessagesException.EXPENSE_AMOUNT_EMPTY);
        }
    }
}
