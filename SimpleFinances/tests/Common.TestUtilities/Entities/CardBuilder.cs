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

        public static IList<Card> Collection(User user, uint count = 2)
        {
            var cards = new List<Card>();

            if(count == 0) count = 1;

            var cardId = 1;

            for(int i = 0; i < count; i++)
            {
                var fakeCard = Build(user);
                fakeCard.CardId = cardId++;

                cards.Add(fakeCard);
            }

            return cards;
        }

        public static Card Build(User user) 
        {
            var card = new Faker<Card>()
                .RuleFor(card => card.CardId, f => f.IndexFaker)
                .RuleFor(card => card.CardGuid, f => new Guid())
                .RuleFor(card => card.Name, f => f.Finance.AccountName())
                .RuleFor(card => card.TypeName, f => f.PickRandom(new[] { "Crédito", "Débito" }))
                .RuleFor(card => card.Bank, f => f.Company.CompanyName())
                .RuleFor(card => card.Limit, f => f.Random.Decimal(0, 50000))
                .RuleFor(card => card.CardDueDate, f => f.Date.Future())
                .RuleFor(card => card.CardClosingDate, f => f.Date.Future())
                .RuleFor(card => card.UserId, f =>  user.UserId);

            return card;         
        }
    }
}
