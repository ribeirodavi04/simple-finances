﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Communication.Responses
{
    public class ResponseRegisteredUserJson
    {
        public string Name { get; set; } = string.Empty;

        public ResponseTokensJson Tokens { get; set; } = default!;
    }
}
