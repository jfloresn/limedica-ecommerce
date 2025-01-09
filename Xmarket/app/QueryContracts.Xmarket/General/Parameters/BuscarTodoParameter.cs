using QueryContracts.Common;
using System;

namespace QueryContracts.Xmarket.General.Parameters
{
    public class BuscarTodoParameter : QueryParameter
    {
        public string Filtro { get; set; }
        public Int32 CodigoRol { get; set; }
        public Int32 RegistroInicio { get; set; }
        public Int32 RegistroFin { get; set; }
        public Int32 TotalRegistros { get; set; }

    }
}