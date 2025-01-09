using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;
using System.Web.Cors;

[assembly: OwinStartup(typeof(Web.Xmarket.Startup))]

namespace Web.Xmarket
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Permitir CORS para todas las solicitudes
            app.UseCors(CorsOptions.AllowAll);

            // Configurar SignalR
            app.MapSignalR();
        }
    }
}
