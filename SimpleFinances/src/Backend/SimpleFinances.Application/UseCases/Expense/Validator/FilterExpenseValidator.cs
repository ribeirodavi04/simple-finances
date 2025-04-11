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
    public class FilterExpenseValidator : AbstractValidator<RequestFilterExpenseJson>
    {
        public FilterExpenseValidator() 
        {
            //RuleFor(filter => filter).Must(filter =>
            //    (filter.StartDate.HasValue && filter.EndDate.HasValue) ||
            //    (!filter.StartDate.HasValue && !filter.EndDate.HasValue))
            //    .WithMessage(ResourceMessagesException.FILTER_EXPENSE_DATES_INVALID);
            
            //RuleFor(filter => filter).Must(x => x.ValueFrom <= x.ValueTo || x.ValueTo == 0).WithMessage(ResourceMessagesException.FILTER_EXPENSE_DATES_INVALID);
        }
    }
}
