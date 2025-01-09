
using QueryContracts.Common;
namespace QueryContracts.Xmarket.Seguridad.Parameters
{
    public class AutorizacionRolParameter : QueryParameter
    {
        private string _nu_op_despachador;
        private string _nu_orden_retiro;
        private string _nu_orden_salida;
        private string _ruc_agencia;
        private string _tipo_usuario;

        public AutorizacionRolParameter()
        {

        }


        public string numero_op_despachador
        {
            get
            {
                return _nu_op_despachador;
            }
            set
            {
                _nu_op_despachador = value;
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

        public string numero_orden_salida
        {
            get
            {
                return _nu_orden_salida;
            }
            set
            {
                _nu_orden_salida = value;
            }
        }

        

        public string ruc_agencia
        {
            get
            {
                return _ruc_agencia;
            }
            set
            {
                _ruc_agencia = value;
            }
        }

        public string tipo_usuario
        {
            get
            {
                return _tipo_usuario;
            }
            set
            {
                _tipo_usuario = value;
            }
        }
    }
}
