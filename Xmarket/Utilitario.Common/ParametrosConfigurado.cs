using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitario.Common
{
    public static class ParametrosConfigurado
    {


        public static string CorreoDestinatario { get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["CorreoDestinatario"]); } }

        public static Int32 COMPAGINADO
        {
            get
            {
              var valor=Convert.ToInt32( System.Configuration.ConfigurationManager.AppSettings["COMPAGINADO"].ToString());
                return valor;
            }
        }

     
    }
}