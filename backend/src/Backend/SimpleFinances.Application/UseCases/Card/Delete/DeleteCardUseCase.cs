using AutoMapper;
using SimpleFinances.Domain.Repositories.Card;
using SimpleFinances.Domain.Repositories;
using SimpleFinances.Domain.Services.LoggedUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFinances.Exceptions.ExceptionsBase;
using SimpleFinances.Exceptions;

namespace SimpleFinances.Application.UseCases.Card.Delete
{
    public class DeleteCardUseCase : IDeleteCardUseCase
    {
        private readonly ICardWriteOnlyRepository _cardWriteOnlyRepository;
        private readonly ICardReadOnlyRepository _cardReadOnlyRepository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly ILoggedUser _loggedUser;

        public DeleteCardUseCase(
            ICardWriteOnlyRepository cardWriteOnlyRepository, 
            ICardReadOnlyRepository cardReadOnlyRepository, 
            IUnityOfWork unityOfWork,
            ILoggedUser loggedUser)
        {
            _cardWriteOnlyRepository = cardWriteOnlyRepository;
            _cardReadOnlyRepository = cardReadOnlyRepository;
            _unityOfWork = unityOfWork;
            _loggedUser = loggedUser;
        }

        public async Task Execute(int idCard)
        {
            var user = await _loggedUser.User();

            var card = await _cardReadOnlyRepository.GetCardById(user, idCard);

            if (card is null)
                throw new SimpleFinancesException(ResourceMessagesException.CARD_NOT_FOUND);

            await _cardWriteOnlyRepository.Delete(idCard);
            await _unityOfWork.Commit();
        }
    }
}
