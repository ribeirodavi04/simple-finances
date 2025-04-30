using FluentValidation;
using SimpleFinances.Communication.Requests;
using SimpleFinances.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Application.UseCases.Income.Validator
{
    public class IncomeValidator : AbstractValidator<RequestIncomeJson>
    {
        public IncomeValidator() 
        {
            RuleFor(income => income.TypeName).NotEmpty().WithMessage(ResourceMessagesException.INCOME_TYPE_NAME_EMPTY);
        }
    }
}
