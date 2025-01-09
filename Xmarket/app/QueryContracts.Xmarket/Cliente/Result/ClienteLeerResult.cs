
using QueryContracts.Common;
using System;
using System.Collections.Generic;
using QueryContracts.Xmarket.Cliente;


namespace QueryContracts.Xmarket.Cliente.Result
{

    public class ClienteLeerResult : QueryResult
    {
        public ClienteDTO Hit { get; set; }

        public IEnumerable<RolDTO> ListaRoles { get; set; }
    }


}
