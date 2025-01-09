
using QueryContracts.Common;
using System;
using System.Collections.Generic;

namespace QueryContracts.Xmarket.Marca.Result
{

    public class ListarMarcaResult : QueryResult
    {
        public IEnumerable<MarcaDTO> Hits { get; set; }
    }


}
