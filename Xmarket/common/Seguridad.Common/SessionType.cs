namespace Seguridad.Common
{
    using System;
    using System.Security.Principal;

    public class SessionType
    {

        private string _codigo_session;
        private string _nombre_session;
 

        public SessionType()
        {

        }

        ~SessionType()
        {

        }

        public string codigo_session
        {
            get
            {
                return _codigo_session;
            }
            set
            {
                _codigo_session = value;
            }
        }

        public string nombre_session
        {
            get
            {
                return _nombre_session;
            }
            set
            {
                _nombre_session = value;
            }
        }

    }//end SessionType
}