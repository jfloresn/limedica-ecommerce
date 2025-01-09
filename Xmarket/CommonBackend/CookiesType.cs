namespace BaseCommon.Common
{
    using System;
    using System.Security.Principal;

    public class CookiesType
    {

        private string _codigo_cookies;
        private string _nombre_cookies;
        private string _valor_cookies;
        private DateTime _fechaExpira;

        public CookiesType()
        {

        }


        public DateTime fechaExpira
        {
            get
            {
                return _fechaExpira;
            }
            set
            {
                _fechaExpira = value;
            }
        }

        public string codigo_cookies
        {
            get
            {
                return _codigo_cookies;
            }
            set
            {
                _codigo_cookies = value;
            }
        }

        public string nombre_cookies
        {
            get
            {
                return _nombre_cookies;
            }
            set
            {
                _nombre_cookies = value;
            }
        }

        public string valor_cookies
        {
            get
            {
                return _valor_cookies;
            }
            set
            {
                _valor_cookies = value;
            }
        }

    }//end CookiesType

}