using AutoMapper;
using SimpleFinances.Communication.Requests;
using SimpleFinances.Communication.Responses;
using SimpleFinances.Domain.Repositories;
using SimpleFinances.Domain.Repositories.Card;
using SimpleFinances.Domain.Services.LoggedUser;
using SimpleFinances.Exceptions;
using SimpleFinances.Exceptions.ExceptionsBase;

namespace SimpleFinances.Application.UseCases.Card.Register
{
    public class RegisterCardUseCase : IRegisterCardUseCase
    {
        private readonly ICardWriteOnlyRepository _cardWriteOnlyRepository;
        private readonly ICardReadOnlyRepository _cardReadOnlyRepository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;

        public RegisterCardUseCase(ICardWriteOnlyRepository cardWriteOnlyRepository, ICardReadOnlyRepository cardReadOnlyRepository, IMapper mapper, IUnityOfWork unityOfWork, ILoggedUser loggedUser)
        {
            _cardWriteOnlyRepository = cardWriteOnlyRepository;
            _cardReadOnlyRepository = cardReadOnlyRepository;
            _mapper = mapper;
            _unityOfWork = unityOfWork;
            _loggedUser = loggedUser;
        }

        public async Task<ResponseRegisteredCardJson> Execute(RequestRegisterCardJson requestCard)
        {
            var loggedUser = await _loggedUser.User();

            await Validate(requestCard, loggedUser.UserId);

            var card = _mapper.Map<Domain.Entities.Card>(requestCard);
            card.CardGuid = Guid.NewGuid();
            card.UserId = loggedUser.UserId;

            await _cardWriteOnlyRepository.Add(card);
            await _unityOfWork.Commit();

            return _mapper.Map<ResponseRegisteredCardJson>(card);
        }

        private async Task Validate(RequestRegisterCardJson requestCard, int userId)
        {
            var validator = new RegisterCardValidator();
            var result = validator.Validate(requestCard);

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
