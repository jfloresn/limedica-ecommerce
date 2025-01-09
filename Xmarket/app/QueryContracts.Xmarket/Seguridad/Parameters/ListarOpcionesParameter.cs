

using QueryContracts.Common;
using System;

namespace QueryContracts.Xmarket.Seguridad.Parameters
{
    public class ListarOpcionesParameter : QueryParameter
    {
        public Int32 CodigoPagina { get; set; }
        public string Codigo { get; set; }
        public string NombrePagina { get; set; }
        public Int32 RegistroInicial { get; set; }
        public Int32 RegistroFinal { get; set; }

    }
}
