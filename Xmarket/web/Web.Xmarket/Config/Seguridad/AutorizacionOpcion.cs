using Seguridad.Common;
using QueryContracts.Common.Seguridad.Parameters;
using QueryContracts.Common.Seguridad.Results;
using ServiceAgents.Common;

namespace Web.Xmarket.Seguridad
{
    public class AutorizacionOpcion : Autorizacion
    {
        public override bool verificar_autorizacion(string codigo_opcion)
        {
            
            VerificarOpcionesParameter o_VerificarOpciones = new VerificarOpcionesParameter();
            o_VerificarOpciones.CodigoOpcion = codigo_opcion;
        

            var o_VerificarOpcionesResult = (VerificarOpcionesResult)o_VerificarOpciones.Execute();

            if (o_VerificarOpcionesResult.Estado == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}