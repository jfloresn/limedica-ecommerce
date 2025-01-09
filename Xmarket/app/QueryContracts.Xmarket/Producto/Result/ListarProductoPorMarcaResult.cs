
using QueryContracts.Common;
using System;
using System.Collections.Generic;

namespace QueryContracts.Xmarket.Producto.Result
{

    public class ListarProductoPorMarcaResult : QueryResult
    {
        public IEnumerable<ProductoDTO> Hits { get; set; }

        public int TotalRegistro { get; set; }

    }


}
