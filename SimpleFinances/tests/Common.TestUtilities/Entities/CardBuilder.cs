using Bogus;
using SimpleFinances.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.TestUtilities.Entities
{
    public class CardBuilder
    {
        public Card Build() 
        {
            var card = new Faker<Card>()
                .RuleFor(card => card.CardId, f => f.IndexFaker)
                .RuleFor(card => card.CardGuid, f => new Guid())
                .RuleFor(card => card.Name, f => f.Finance.AccountName())
                .RuleFor(card => card.TypeName, f => f.PickRandom(new[] { "Crédito", "Débito" }))
                .RuleFor(card => card.Bank, f => f.Company.CompanyName())
                .RuleFor(card => card.Limit, f => f.Random.Decimal(0, 50000))
                .RuleFor(card => card.CardDueDate, f => f.Date.Future())
                .RuleFor(card => card.CardClosingDate, f => f.Date.Future());

            return card;         
        }
    }
}
