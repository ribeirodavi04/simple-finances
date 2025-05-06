using FluentValidation;
using SimpleFinances.Communication.Requests;
using SimpleFinances.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Application.UseCases.Expense.Validator
{
    public class ExpenseValidator : AbstractValidator<RequestExpenseJson>
    {
        public ExpenseValidator() 
        {
            RuleFor(expense => expense.Name).NotEmpty().WithMessage(ResourceMessagesException.EXPENSE_NAME_EMPTY);
            RuleFor(expense => expense.TypeName).NotEmpty().WithMessage(ResourceMessagesException.EXPENSE_TYPE_NAME_EMPTY);
            RuleFor(expense => expense.Amount).NotEmpty().WithMessage(ResourceMessagesException.EXPENSE_AMOUNT_EMPTY);
        }
    }
}
