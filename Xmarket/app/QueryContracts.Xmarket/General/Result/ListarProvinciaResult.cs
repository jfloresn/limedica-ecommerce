
using QueryContracts.Common;
using System;
using System.Collections.Generic;

namespace QueryContracts.Xmarket.General.Result
{

    public class ListarProvinciaResult : QueryResult
    {
        public IEnumerable<ProvinciaDTO> Hits { get; set; }
    }


}
