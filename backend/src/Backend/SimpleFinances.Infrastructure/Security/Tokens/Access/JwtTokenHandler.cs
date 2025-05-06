using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Infrastructure.Security.Tokens.Access
{
    public abstract class JwtTokenHandler
    {
        protected SymmetricSecurityKey SecurityKey(string key)
        {
            var bytes = Encoding.UTF8.GetBytes(key);
            return new SymmetricSecurityKey(bytes); 
        }
    }
}
