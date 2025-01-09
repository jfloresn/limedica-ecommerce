
using QueryContracts.Common;
using System.Collections.Generic;
namespace QueryContracts.Xmarket.Seguridad.Result
{

    public class AutorizacionRolResult : QueryResult
    {

        private bool _autorizado_generar;
        private string _nu_orden_retiro;

        public AutorizacionRolResult()
        {

        }

        ~AutorizacionRolResult()
        {

        }

        public bool autorizado_generar
        {
            get
            {
                return _autorizado_generar;
            }
            set
            {
                _autorizado_generar = value;
            }
        }

        public string numero_orden_retiro
        {
            get
            {
                return _nu_orden_retiro;
            }
            set
            {
                _nu_orden_retiro = value;
            }
        }

    }//end AutorizacionRolResult
}
