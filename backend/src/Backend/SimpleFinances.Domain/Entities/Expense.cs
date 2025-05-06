using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Domain.Entities
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string TypeName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public bool IsRecurring { get; set; }
        public DateTime? DateExpense { get; set; }
        public string Description { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int? CardId { get; set; }
    }
}
