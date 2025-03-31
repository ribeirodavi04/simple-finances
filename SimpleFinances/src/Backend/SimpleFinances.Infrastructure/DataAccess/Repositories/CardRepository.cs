using Microsoft.EntityFrameworkCore;
using SimpleFinances.Domain.Entities;
using SimpleFinances.Domain.Repositories.Card;
using SimpleFinances.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SimpleFinances.Infrastructure.DataAccess.Repositories
{
    public class CardRepository : ICardReadOnlyRepository, ICardWriteOnlyRepository, ICardUpdateOnlyRepository
    {
        private readonly SimpleFinancesDbContext _dbContext;

        public CardRepository(SimpleFinancesDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }
        
        public async Task Add(Card card)
        {
            await _dbContext.Cards.AddAsync(card);
        }

        public async Task Delete(int idCard)
        {
            var card = await _dbContext.Cards.FindAsync(idCard);
            _dbContext.Cards.Remove(card!);  
        }

        public async Task<bool> ExistCardName(string name, int userId)
        {
            return await _dbContext.Cards.AsNoTracking().AnyAsync(card => string.Equals(card.Name, name) && card.UserId == userId);
        }

        public Task<IList<Card>> Filter()
        {
            throw new NotImplementedException();
        }

        async Task<Card?> ICardReadOnlyRepository.GetCardById(User user, int idCard)
        {
            return await _dbContext.Cards.AsNoTracking().FirstOrDefaultAsync(card => card.UserId == user.UserId && card.CardId == idCard);
        }

        async Task<Card?> ICardUpdateOnlyRepository.GetCardById(User user, int idCard)
        {
            return await _dbContext.Cards.FirstOrDefaultAsync(card => card.UserId == user.UserId && card.CardId == idCard);
        }

        public void Update(Card card)
        {
            _dbContext.Cards.Update(card);
        }

    }
}
