using SimpleFinances.Communication.Requests;

namespace SimpleFinances.Application.UseCases.Card.Update
{
    public interface IUpdateCardUseCase
    {
        public Task Execute(int idCard, RequestCardJson requestCard);
    }
}
