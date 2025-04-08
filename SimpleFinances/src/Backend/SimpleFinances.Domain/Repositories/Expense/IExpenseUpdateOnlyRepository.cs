using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Domain.Repositories.Expense
{
    public interface IExpenseUpdateOnlyRepository
    {
        Task<Entities.Expense?> GetExpenseById(Entities.User user, int idExpense);
        void Update(Entities.Expense expense);
    }
}
