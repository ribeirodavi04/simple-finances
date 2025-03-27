using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Domain.Security.Tokens
{
    public interface ITokenProvider
    {
        public string Value();
    }
}
