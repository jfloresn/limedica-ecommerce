using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.Xmarket.Seguridad.Result
{
    public class ListarOpcionesPaginasResult : EliminarPaginaResult
    {
        public IEnumerable<OpcionesPaginaDTO> Hits { get; set; }
        public Int32 TotalRegistros { get; set; }
    }

    public class OpcionesPaginaDTO
    {
        public Int32 CodigoRol_RPO { get; set; }
        public Int32 CodigoPagina_RPO { get; set; }
        public string CodigoOpcion_RPO { get; set; }
        public string ID_OPC { get; set; }
        public string Codigo_OPC { get; set; }
        public string NombreCodigo_OPC { get; set; }
        public bool Seleccionado { get; set; }

    }
}
