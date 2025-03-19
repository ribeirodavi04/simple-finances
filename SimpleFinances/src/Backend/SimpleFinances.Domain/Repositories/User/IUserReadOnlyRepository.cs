using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Domain.Repositories.User
{
    public interface IUserReadOnlyRepository
    {
        public Task<bool> ExistActiveUserWithEmail(string email);
        public Task<bool> ExistActiveUserWithIdentifier(Guid userGuid);
        public Task<Entities.User?> GetUserByEmailAndPassword(string email, string password);
    }
}
