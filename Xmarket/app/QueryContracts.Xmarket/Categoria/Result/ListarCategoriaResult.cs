
using QueryContracts.Common;
using System;
using System.Collections.Generic;

namespace QueryContracts.Xmarket.Categoria.Result
{

    public class ListarCategoriaResult : QueryResult
    {
        public IEnumerable<CategoriaDTO> Hits { get; set; }
    }


}
