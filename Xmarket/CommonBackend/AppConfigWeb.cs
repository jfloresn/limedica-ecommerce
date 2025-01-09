using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Common
{
    public static  class AppConfigWeb
    {

        public static string  serverName = ConfigurationManager.AppSettings["servername"];


    }
}
