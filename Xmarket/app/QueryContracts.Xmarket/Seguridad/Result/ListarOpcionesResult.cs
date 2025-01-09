

using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.Xmarket.Seguridad.Result
{
    public class ListarOpcionesResult : QueryResult
    {
        public IEnumerable<OpcionesDTO> Hits { get; set; }
        public Int32 TotalRegistros { get; set; }
        
    }

}
