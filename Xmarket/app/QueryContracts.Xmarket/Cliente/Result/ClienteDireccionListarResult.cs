﻿
using QueryContracts.Common;
using QueryContracts.Xmarket.Account.Results;
using System;
using System.Collections.Generic;

namespace QueryContracts.Xmarket.Cliente.Result
{

    public class ClienteDireccionListarResult : QueryResult
    {
    
        public IEnumerable<ClienteDireccionDTO> direcciones { get; set; }
    }

}