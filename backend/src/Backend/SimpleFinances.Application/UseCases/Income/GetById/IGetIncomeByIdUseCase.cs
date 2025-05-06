using SimpleFinances.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Application.UseCases.Income.GetById
{
    public interface IGetIncomeByIdUseCase
    {
        public Task<ResponseIncomeJson> Execute(int idIncome);
    }
}
