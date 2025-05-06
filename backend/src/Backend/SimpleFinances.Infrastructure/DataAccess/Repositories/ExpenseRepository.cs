using Microsoft.EntityFrameworkCore;
using SimpleFinances.Domain.DTOs;
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

        public async Task<IList<Expense>> Filter(User user, FilterExpenseDTO filters)
        {
            var query = _dbContext
                .Expenses
                .AsNoTracking()
                .Where(expense => expense.UserId == user.UserId);

            if(filters.CardsIds != null && filters.CardsIds.Any())            
                query = query.Where(expense => expense.CardId.HasValue && filters.CardsIds.Contains(expense.CardId.Value));
            
            if(filters.StartDate.HasValue && filters.EndDate.HasValue)            
                query = query.Where(expense => 
                    expense.DateExpense >= filters.StartDate.Value && 
                    expense.DateExpense <= filters.EndDate.Value);

            if(filters.ValueTo.HasValue)
                query = query.Where(expense => expense.Amount <= filters.ValueTo);

            if(filters.ValueFrom.HasValue)
                query = query.Where(expense => expense.Amount >= filters.ValueFrom);

            if(!string.IsNullOrEmpty(filters.Name))
                query = query.Where(expense => expense.Name.Contains(filters.Name));


            if (filters.IsRecurring is not null)
                query = query.Where(expense => expense.IsRecurring == filters.IsRecurring);

            return await query.ToListAsync();
        }
    }
}
