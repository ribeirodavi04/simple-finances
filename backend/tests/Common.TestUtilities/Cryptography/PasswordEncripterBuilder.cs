using SimpleFinances.Application.Services.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.TestUtilities.Cryptography
{
    public class PasswordEncripterBuilder
    {
        public static PasswordEncripter Build() => new PasswordEncripter("teste123");
    }
}
