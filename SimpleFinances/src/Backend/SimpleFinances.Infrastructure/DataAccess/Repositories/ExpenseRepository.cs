using Microsoft.EntityFrameworkCore;
using SimpleFinances.Domain.Entities;
using SimpleFinances.Domain.Repositories.Expense;
using SimpleFinances.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Infrastructure.DataAccess.Repositories
{
    public class ExpenseRepository : IExpenseReadOnlyRepository, IExpenseUpdateOnlyRepository, IExpenseWriteOnlyRepository
    {
        private readonly SimpleFinancesDbContext _dbContext;

        public ExpenseRepository(SimpleFinancesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Expense expense)
        {
            await _dbContext.Expenses.AddAsync(expense);
        }

        public async Task Delete(int idExpense)
        {
            var income = await _dbContext.Expenses.FindAsync(idExpense);
            _dbContext.Expenses.Remove(income!);
        }

        public Task<IList<Expense>> GetAllExpenses(User user)
        {
            throw new NotImplementedException();
        }

        async Task<Expense?> IExpenseReadOnlyRepository.GetExpenseById(User user, int idExpense)
        {
            return await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(expense => expense.ExpenseId == idExpense && expense.UserId == user.UserId);
        }

        async Task<Expense?> IExpenseUpdateOnlyRepository.GetExpenseById(User user, int idExpense)
        {
            return await _dbContext.Expenses.FirstOrDefaultAsync(expense => expense.ExpenseId == idExpense && expense.UserId == user.UserId);
        }

        public void Update(Expense expense)
        {
            _dbContext.Expenses.Update(expense);
        }
    }
}
