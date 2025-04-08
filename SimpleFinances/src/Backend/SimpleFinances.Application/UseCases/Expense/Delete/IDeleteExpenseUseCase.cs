using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Application.UseCases.Expense.Delete
{
    public interface IDeleteExpenseUseCase
    {
        public Task Execute(int idExpense);
    }
}
