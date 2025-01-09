
using QueryContracts.Common;
using System;
using System.Collections.Generic;

namespace QueryContracts.Xmarket.Editorial.Result
{

    public class ListarEditorialResult : QueryResult
    {
        public IEnumerable<EditorialDTO> Hits { get; set; }
    }


}
