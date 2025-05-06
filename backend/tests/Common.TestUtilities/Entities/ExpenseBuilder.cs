using Bogus;
using SimpleFinances.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.TestUtilities.Entities
{
    public class ExpenseBuilder
    {
        public static Expense Build(User user, Card card)
        {
            var expense = new Faker<Expense>()
                .RuleFor(expense => expense.ExpenseId, f => f.IndexFaker)
                .RuleFor(expense => expense.Name, f => f.Commerce.ProductName())
                .RuleFor(expense => expense.TypeName, f => f.Commerce.ProductAdjective())
                .RuleFor(expense => expense.Amount, f => decimal.Parse(f.Commerce.Price()))
                .RuleFor(expense => expense.IsRecurring, f => f.Random.Bool())
                .RuleFor(expense => expense.DateExpense, f => f.Date.Future())
                .RuleFor(expense => expense.Description, f => f.Lorem.Sentence(2))
                .RuleFor(expense => expense.UserId, f => user.UserId)
                .RuleFor(expense => expense.CardId, f => card.CardId);

            return expense;
        }
    }
}
