using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Domain.Entities
{
    public class Cartao
    {
        public int CartaoId { get; set; }
        public Guid CartaoGuid { get; set; }
        public string CartaoNome { get; set; }
        public string CartaoTipo { get; set; }
        public string CartaoBanco { get; set; }
        public decimal CartaoLimite { get; set; }
        public DateTime? CartaoDataVencimento { get; set; }
        public DateTime? CartaoDataFechamento { get; set; }
        public Guid CartaoUsuarioId;

    }
}
