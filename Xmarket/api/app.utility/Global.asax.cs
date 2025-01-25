using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Web.Http;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Web.Xmarket.App_Start;

namespace Web.Xmarket
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            string logPath = Server.MapPath("~/Logs/miapp.log");
            FileInfo logFileInfo = new FileInfo(logPath);

            // Verifica si la carpeta existe, si no la crea
            if (!logFileInfo.Directory.Exists)
            {
                logFileInfo.Directory.Create();
            }

            // Configura log4net con la ruta correcta
            XmlConfigurator.Configure();


        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}