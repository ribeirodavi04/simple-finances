using Microsoft.EntityFrameworkCore;
using SimpleFinances.Domain.Entities;
using SimpleFinances.Domain.Repositories.Income;
using SimpleFinances.Infrastructure.Context;

namespace SimpleFinances.Infrastructure.DataAccess.Repositories
{
    public class IncomeRepository : IIncomeReadOnlyRepository, IIncomeUpdateOnlyRepository, IIncomeWriteOnlyRepository
    {
        private readonly SimpleFinancesDbContext _dbContext;

        public IncomeRepository(SimpleFinancesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Income income)
        {
            await _dbContext.Incomes.AddAsync(income);
        }

        public async Task Delete(int idIncome)
        {
            var income = await _dbContext.Incomes.FindAsync(idIncome);
            _dbContext.Incomes.Remove(income!);
        }

        public async Task<bool> ExistIncomeTypeName(string name, int userId)
        {
            return await _dbContext.Incomes.AsNoTracking().AnyAsync(income => string.Equals(income.TypeName, name) && income.UserId == userId);
        }

        public async Task<IList<Income>> GetAllIncomes(User user)
        {
            return await _dbContext.Incomes.AsNoTracking().Where(income => income.UserId == user.UserId).ToListAsync();
        }

        async Task<Income?> IIncomeReadOnlyRepository.GetIncomeById(User user, int idIncome)
        {
            return await _dbContext.Incomes.AsNoTracking().FirstOrDefaultAsync(income => income.UserId == user.UserId && income.IncomeId == idIncome);
        }

        async Task<Income?> IIncomeUpdateOnlyRepository.GetIncomeById(User user, int idIncome)
        {
            return await _dbContext.Incomes.FirstOrDefaultAsync(income => income.UserId == user.UserId && income.IncomeId == idIncome);
        }

        public void Update(Income income)
        {
            _dbContext.Incomes.Update(income);
        }
    }
}
