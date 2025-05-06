using Microsoft.EntityFrameworkCore;
using SimpleFinances.Domain.Entities;
using SimpleFinances.Domain.Repositories.User;
using SimpleFinances.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
    {
        private readonly SimpleFinancesDbContext _dbContext;

        public UserRepository(SimpleFinancesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }

        public async Task<bool> ExistActiveUserWithEmail(string email)
        {
            return await _dbContext.Users.AnyAsync(user => user.Email == email);
        }

        public async Task<bool> ExistActiveUserWithIdentifier(Guid userGuid)
        {
            return await _dbContext.Users.AnyAsync(user => user.UserGuid == userGuid);
        }

        public async Task<bool> ExistActiveUserWithUserName(string userName)
        {
            return await _dbContext.Users.AnyAsync(user => user.UserName.Equals(userName));
        }

        public async Task<User?> GetUserByEmailAndPassword(string email, string password)
        {
            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email) && user.Password.Equals(password));
        }
    }
}
