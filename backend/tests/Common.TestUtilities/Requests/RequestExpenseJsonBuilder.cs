using Bogus;
using SimpleFinances.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.TestUtilities.Requests
{
    public class RequestExpenseJsonBuilder
    {
        public static RequestExpenseJson Build()
        {
            return new Faker<RequestExpenseJson>()
                .RuleFor(expense => expense.Name, f => f.Commerce.ProductName())
                .RuleFor(expense => expense.TypeName, f => f.Commerce.Department())
                .RuleFor(expense => expense.Amount, f => decimal.Parse(f.Commerce.Price()))
                .RuleFor(expense => expense.IsRecurring, f => f.Random.Bool())
                .RuleFor(expense => expense.DateExpense, f => f.Date.Past())
                .RuleFor(expense => expense.Description, f => f.Lorem.Sentence())
                .RuleFor(expense => expense.CardId, f => f.Random.Int(0, 2000));
        }
    }
}
