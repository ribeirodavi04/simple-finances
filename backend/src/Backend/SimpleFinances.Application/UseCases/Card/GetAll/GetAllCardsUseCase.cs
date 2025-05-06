using AutoMapper;
using SimpleFinances.Communication.Responses;
using SimpleFinances.Domain.Repositories.Card;
using SimpleFinances.Domain.Services.LoggedUser;
using SimpleFinances.Exceptions.ExceptionsBase;
using SimpleFinances.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Application.UseCases.Card.GetAll
{
    public class GetAllCardsUseCase : IGetAllCardsUseCase
    {
        private readonly ICardReadOnlyRepository _cardReadOnlyRepository;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;

        public GetAllCardsUseCase(ICardReadOnlyRepository cardReadOnlyRepository, IMapper mapper, ILoggedUser loggedUser)
        {
            _cardReadOnlyRepository = cardReadOnlyRepository;
            _mapper = mapper;
            _loggedUser = loggedUser;
        }

        public async Task<IList<ResponseCardJson>> Execute()
        {
            var user = await _loggedUser.User();

            var cards = await _cardReadOnlyRepository.GetAllCards(user);

            if (cards is null)
                throw new SimpleFinancesException(ResourceMessagesException.CARD_NOT_FOUND);

            return _mapper.Map<IList<ResponseCardJson>>(cards);
        }
    }
}
