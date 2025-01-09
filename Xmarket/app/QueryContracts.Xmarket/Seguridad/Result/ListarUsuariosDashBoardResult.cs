
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.Xmarket.Seguridad.Result
{
    public class ListarUsuariosDashBoardResult : EliminarPaginaResult
    {
        public IEnumerable<ListarUsuariosDto> Hits { get; set; }

        public Int32 totalregistros { get; set; }

        
    }

}
