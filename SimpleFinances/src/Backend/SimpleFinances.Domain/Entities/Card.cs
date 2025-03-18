using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Domain.Entities
{
    public class Card
    {
        public int CardId { get; set; }
        public Guid CardGuid { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public string Bank { get; set; }
        public decimal Limit { get; set; }
        public DateTime? CardDueDate { get; set; }
        public DateTime? CardClosingDate { get; set; }
        public int UserId { get; set; }
    }
}
