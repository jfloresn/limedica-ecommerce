namespace BaseCommon.Common
{
    public class ErrorType
    {

        private int nu_error;
        private int nu_severidad;
        private string tx_descripcion;

        public ErrorType()
        {

        }

        public string descripcion
        {
            get
            {
                return tx_descripcion;
            }
            set
            {
                tx_descripcion = value;
            }
        }

        public int numero_error
        {
            get
            {
                return nu_error;
            }
            set
            {
                nu_error = value;
            }
        }

        public int numero_severidad
        {
            get
            {
                return nu_severidad;
            }
            set
            {
                nu_severidad = value;
            }
        }

    }
}