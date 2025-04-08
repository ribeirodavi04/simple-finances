using SimpleFinances.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Application.UseCases.Expense.GetById
{
    public interface IGetExpenseByIdUseCase
    {
        public Task<ResponseExpenseJson> Execute(int idExpense);
    }
}
