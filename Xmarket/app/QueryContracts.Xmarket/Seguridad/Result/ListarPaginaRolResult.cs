
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.Xmarket.Seguridad.Result
{
    public class ListarPaginaRolResult : EliminarPaginaResult
    {
        public IEnumerable<PaginaROl> Hits { get; set; }

        public Int32 TotalRegistros { get; set; }
    }

    public class PaginaROl
    {
        public Int32 CodigoRol { get; set; }
        public Int32 CodigoPagina { get; set; }
        public string CodigoPermiso { get; set; }
        public string CodigoMenuPagina { get; set; }

        public string CodigoMenuPaginaPadre { get; set; }
        
        public string NombrePagina { get; set; }
        public string ActionPagina { get; set; }
        public string ControllerPagina { get; set; }
        public string Atributopagina { get; set; }
        public string TipoMenuPagina { get; set; }
    }
}