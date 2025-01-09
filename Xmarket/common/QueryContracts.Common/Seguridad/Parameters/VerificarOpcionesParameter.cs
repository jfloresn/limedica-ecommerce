

using QueryContracts.Common;
using System;

namespace QueryContracts.Common.Seguridad.Parameters
{
    public class VerificarOpcionesParameter : QueryParameter
    {

        public string CodigoOpcion { get; set; }
        public Int32? CodigoRol { get; set; }

    }
}
