﻿using FluentValidation;
using SimpleFinances.Communication.Requests;
using SimpleFinances.Exceptions;

namespace SimpleFinances.Application.UseCases.Card.Register
{
    public class RegisterCardValidator : AbstractValidator<RequestRegisterCardJson>
    {
        public RegisterCardValidator() 
        { 
            RuleFor(card => card.Name).NotEmpty().WithMessage(ResourceMessagesException.CARD_NAME_EMPTY);
            RuleFor(card => card.TypeName).NotEmpty().WithMessage(ResourceMessagesException.CARD_TYPE_EMPTY);
        }
    }
}
