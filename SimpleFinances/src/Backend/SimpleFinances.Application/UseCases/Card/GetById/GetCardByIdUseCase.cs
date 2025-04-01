using AutoMapper;
using SimpleFinances.Communication.Responses;
using SimpleFinances.Domain.Repositories.Card;
using SimpleFinances.Domain.Services.LoggedUser;
using SimpleFinances.Exceptions;
using SimpleFinances.Exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Application.UseCases.Card.GetById
{
    public class GetCardByIdUseCase : IGetCardByIdUseCase
    {
        private readonly ICardReadOnlyRepository _cardReadOnlyRepository;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;

        public GetCardByIdUseCase(ICardReadOnlyRepository cardReadOnlyRepository, IMapper mapper, ILoggedUser loggedUser)
        {
            _cardReadOnlyRepository = cardReadOnlyRepository;
            _mapper = mapper;
            _loggedUser = loggedUser;
        }

        public async Task<ResponseCardJson> Execute(int idCard)
        {
            var user = await _loggedUser.User();

            var card = await _cardReadOnlyRepository.GetCardById(user, idCard);

            if (card is null)
                throw new SimpleFinancesException(ResourceMessagesException.CARD_NOT_FOUND);

            return _mapper.Map<ResponseCardJson>(card);
        }

    }
}
