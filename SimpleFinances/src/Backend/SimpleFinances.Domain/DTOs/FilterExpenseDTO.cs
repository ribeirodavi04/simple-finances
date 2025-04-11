using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Domain.DTOs
{
    public record FilterExpenseDTO
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Name { get; set; }
        public IList<int>? CardsIds { get; set; }
        public decimal? ValueFrom { get; set; }
        public decimal? ValueTo { get; set; }
        public bool? IsRecurring { get; set; }
    }
}
