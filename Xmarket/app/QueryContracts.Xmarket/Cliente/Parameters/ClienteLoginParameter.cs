using QueryContracts.Common;
using System;
using System.Collections.Generic;

namespace QueryContracts.Xmarket.Cliente.Parameters
{
    public class ClienteLoginParameter : QueryParameter
    {
        public string Correo { get; set; }
        public string Clave { get; set; }
        public string Token { get; set; }

    }
}
