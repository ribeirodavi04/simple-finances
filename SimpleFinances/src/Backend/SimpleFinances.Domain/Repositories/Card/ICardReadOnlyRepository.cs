using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Domain.Repositories.Card
{
    public interface ICardReadOnlyRepository
    {
        public Task<IList<Domain.Entities.Card>> Filter();
        public Task<Domain.Entities.Card?> GetCardById(Domain.Entities.User user, int idCard);
        public Task<bool> ExistCardName(string name, int userId);

    }
}
