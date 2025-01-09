
using QueryContracts.Common;
using System;
using System.Collections.Generic;

namespace QueryContracts.Xmarket.General.Result
{

    public class ListarDepartamentosResult : QueryResult
    {
        public IEnumerable<DepartamentoDTO> Hits { get; set; }
    }


}
