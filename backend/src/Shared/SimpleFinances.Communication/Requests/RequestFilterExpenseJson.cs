using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Communication.Requests
{
    public class RequestFilterExpenseJson
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Name { get; set; } = string.Empty;          
        public IList<int>? CardsIds { get; set; }
        public decimal? ValueFrom { get; set; }
        public decimal? ValueTo { get; set; }
        public bool? IsRecurring { get; set; }

    }
}
