using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Domain.Repositories.Expense
{
    public interface IExpenseReadOnlyRepository
    {
        public Task<IList<Domain.Entities.Expense>> GetAllExpenses(Domain.Entities.User user);
        public Task<Domain.Entities.Expense?> GetExpenseById(Domain.Entities.User user, int idExpense);
    }
}
