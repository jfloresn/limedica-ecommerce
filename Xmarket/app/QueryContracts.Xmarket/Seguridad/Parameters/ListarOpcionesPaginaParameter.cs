
using QueryContracts.Common;
using System;

namespace QueryContracts.Xmarket.Seguridad.Parameters
{
    public class ListarOpcionesPaginaParameter : QueryParameter
    {
        public Int32 CodigoRol { get; set; }
        public Int32 CodigoPagina { get; set; }

        public Int32 RegistroInicio { get; set; }
        public Int32 RegistroFin { get; set; }
        public Int32 TotalRegistros { get; set; }

        

    }
}