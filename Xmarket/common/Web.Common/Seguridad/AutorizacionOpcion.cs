using Seguridad.Common;
using QueryContracts.Common.Seguridad.Parameters;
using QueryContracts.Common.Seguridad.Results;
using ServiceAgents.Common;

namespace Web.Common.Seguridad
{
    public class AutorizacionOpcion : Autorizacion
    {
        public override bool verificar_autorizacion(string codigo_opcion)
        {
        

            
            
                return false;
            
        }
    }
}