using SimpleFinances.Communication.Requests;
using SimpleFinances.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Application.UseCases.Income.Register
{
    public interface IRegisterIncomeUseCase
    {
        public Task<ResponseIncomeJson> Execute(RequestIncomeJson requestIncome);
    }
}
