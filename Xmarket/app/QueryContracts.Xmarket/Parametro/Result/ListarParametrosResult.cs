
using QueryContracts.Common;
using System;
using System.Collections.Generic;

namespace QueryContracts.Xmarket.Parametro.Result
{

    public class ListarParametrosResult : QueryResult
    {
        public IEnumerable<ParametroDTO> Hits { get; set; }



    }


}
