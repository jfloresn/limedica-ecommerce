
using QueryContracts.Common;
using System;
using System.Collections.Generic;

namespace QueryContracts.Xmarket.Cliente.Result
{

    public class ClienteLoginResult : QueryResult
    {
        public IEnumerable<ClienteDTO> Hits { get; set; }
    }


}
