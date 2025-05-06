using SimpleFinances.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Application.UseCases.Income.Update
{
    public interface IUpdateIncomeUseCase
    {
        public Task Execute(int idIncome, RequestIncomeJson requestIncome);
    }
}
