
using QueryContracts.Common;
using System;
using System.Collections.Generic;

namespace QueryContracts.Xmarket.General.Result
{

    public class ListarDistritoResult : QueryResult
    {
        public IEnumerable<DistritoDTO> Hits { get; set; }
    }


}
