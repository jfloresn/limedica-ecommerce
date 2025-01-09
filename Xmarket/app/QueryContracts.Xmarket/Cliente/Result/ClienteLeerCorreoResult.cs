
using QueryContracts.Common;
using QueryContracts.Xmarket.Account.Results;
using System;
using System.Collections.Generic;

namespace QueryContracts.Xmarket.Cliente.Result
{

    public class ClienteLeerCorreoResult : QueryResult
    {
        public ClienteDTO Hit { get; set; }

        public IEnumerable<RolDTO> ListaRoles { get; set; }
    }

}
