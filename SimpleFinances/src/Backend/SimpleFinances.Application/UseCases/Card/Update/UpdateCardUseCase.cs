using AutoMapper;
using SimpleFinances.Domain.Repositories.Card;
using SimpleFinances.Domain.Repositories;
using SimpleFinances.Domain.Services.LoggedUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFinances.Communication.Requests;
using SimpleFinances.Exceptions.ExceptionsBase;
using SimpleFinances.Exceptions;
using SimpleFinances.Application.UseCases.Card.Validator;

namespace SimpleFinances.Application.UseCases.Card.Update
{
    public class UpdateCardUseCase : IUpdateCardUseCase
    {
        private readonly ICardUpdateOnlyRepository _cardUpdateOnlyRepository;
        private readonly ICardReadOnlyRepository _cardReadOnlyRepository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;

        public UpdateCardUseCase(
            ICardUpdateOnlyRepository cardUpdateeOnlyRepository,
            ICardReadOnlyRepository cardReadOnlyRepository, 
            IUnityOfWork unityOfWork, 
            IMapper mapper, 
            ILoggedUser loggedUser)
        {
            _cardUpdateOnlyRepository = cardUpdateeOnlyRepository;
            _cardReadOnlyRepository = cardReadOnlyRepository;
            _unityOfWork = unityOfWork;
            _mapper = mapper;
            _loggedUser = loggedUser;
        }

        public async Task Execute(int idCard, RequestCardJson requestCard)
        {
            var loggedUser = await _loggedUser.User();
            await Validate(requestCard, loggedUser.UserId);

            var card = await _cardUpdateOnlyRepository.GetCardById(loggedUser, idCard);

            if (card is null)
                throw new SimpleFinancesException(ResourceMessagesException.CARD_NOT_FOUND);

            _mapper.Map(requestCard, card);

            _cardUpdateOnlyRepository.Update(card);
            await _unityOfWork.Commit();
        }

        private async Task Validate(RequestCardJson requestCard, int userId)
        {
            var result = new CardValidator().Validate(requestCard);

            var cardNameExist = await _cardReadOnlyRepository.ExistCardName(requestCard.Name, userId);

            if (cardNameExist)
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.CARD_NAME_ALREADY_EXIST));

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }

        }

    }
}
