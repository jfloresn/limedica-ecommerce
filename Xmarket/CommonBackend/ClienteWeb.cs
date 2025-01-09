using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BaseCommon.Common
{
    public class ClienteWeb
    {
        public string ObtenerIpPublic()
        {
            string clientIp = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
                           HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim();
            return clientIp;
        }

        public string getCodigoSesionPublico()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
