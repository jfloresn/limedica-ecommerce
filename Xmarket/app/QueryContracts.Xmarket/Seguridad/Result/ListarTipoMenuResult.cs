
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.Xmarket.Seguridad.Result
{
    public class ListarTipoMenuResult : EliminarPaginaResult
    {
        public IEnumerable<TipoMenuDTO> Hits { get; set; }

    }

    public class TipoMenuDTO
    {
         public string CodigoTipoMenu { get; set; }
         public string NombreTipoMenu { get; set; }
         public string Estado  { get; set; }
         public string Eliminado { get; set; }
    }
}