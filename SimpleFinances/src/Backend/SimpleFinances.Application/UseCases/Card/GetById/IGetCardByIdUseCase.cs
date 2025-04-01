using SimpleFinances.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Application.UseCases.Card.GetById
{
    public interface IGetCardByIdUseCase
    {
        public Task<ResponseCardJson> Execute(int idCard);
    }
}
