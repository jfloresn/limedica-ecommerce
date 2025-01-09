
using QueryContracts.Common;
using System;
using System.Collections.Generic;

namespace QueryContracts.Xmarket.General.Result
{

    public class BuscarTodoResult : QueryResult
    {
        public IEnumerable<BuscarTodoDTO> Hits { get; set; }

        public Int32 TotalRegistros { get; set; }

    }


}
