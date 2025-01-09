namespace Web.Common.HttpApplications
{
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Reflection;
    using log4net;
    using System.Web;
    using Web.Common.HttpApplications.AppConfig;
    using Bootstrapper.Web.Common;
    using System;
    using System.Net;
    using StructureMap;
    using System.Web.Optimization;
    using System.Globalization;

    public class CommonHttpApplication : HttpApplication
    {
        private  readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected virtual void Application_Start()
        {
            LoginProviderConfig.RegisterLoggingProviders();
            WebStartUp.Init();
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                "Cuenta",
                "cuenta",
                new { controller = "Cuenta", action = "Index" });


            routes.MapRoute(
                "MiCuenta",
                "cuenta/mi-cuenta",
                new { controller = "Cuenta", action = "MiCuenta" });


            routes.MapRoute(
                "Carrito",
                "carrito",
                new { controller = "Carrito", action = "Index" });

            routes.MapRoute(
                "BookDetail",
                "libro/{title}/isbn-{isbn}",
                new { controller = "Producto", action = "productTitle", title = "", isbn = "" }
            );

            routes.MapRoute(
               "TiendaEBook",
               "tienda/libros-de-e-book-en-peru",
               new { controller = "tienda", action = "ebook" }
           );

            routes.MapRoute(
               "TiendaEditorial",
               "tienda/libros-de-editorial-{editorial}-en-peru-{id}",
               new { controller = "tienda", action = "editorial", editorial = "", id = "" }
           );
            routes.MapRoute(
   "TiendaEditorialOpcion2",
   "tienda/libros-de-editorial-{editorial}-en-peru/{id}",
   new { controller = "tienda", action = "editorial", editorial = "", id = "" }
);
            routes.MapRoute(
               "TiendaEspecialidad",
               "tienda/libros-de-{especialidad}-en-peru-{id}",
               new { controller = "tienda", action = "especialidad", especialidad = "", id = "" }
           );


            routes.MapRoute(
               "TiendaEspecialidadOpcion2",
               "tienda/libros-de-{especialidad}-en-peru/{id}",
               new { controller = "tienda", action = "especialidad", especialidad = "", id = "" }
           );

            routes.MapRoute(
               "TiendaNovedades",
               "tienda/novedades-en-libros-de-medicina-en-peru",
               new { controller = "tienda", action = "novedades" }
           );

            routes.MapRoute(
               "TiendaBuscar",
               "tienda/buscar-libros-de-medicina-en-peru",
               new { controller = "tienda", action = "buscar" }
           );
            routes.MapRoute(
          "PreguntasFrecuentes",
          "preguntas-frecuentes",
          new { controller = "PreguntasFrecuentes", action = "Index" });

            routes.MapRoute(
                "Nosotros",
                "nosotros",
                new { controller = "Nosotros", action = "Index" });
            routes.MapRoute(
                "Contactenos",
                "contactenos",
                new { controller = "Contactenos", action = "Index" });

            routes.MapRoute(
                "Catalogo",
                "catalogo",
                new { controller = "Catalogo", action = "Index" });

            routes.MapRoute(
                "Coleccion",
                "coleccion",
                new { controller = "Colecciones", action = "Index" });

            routes.MapRoute(
                      "ColeccionPorId",
                      "coleccion/{nombre}/libros/{id}",
                      new { controller = "Colecciones", action = "libros", nombre = "", id = "" }
                  );
        }

        protected virtual void Application_Error(object sender, EventArgs e)
        {
            Exception exception = this.Server.GetLastError().GetBaseException();
            HttpException httpException = exception as HttpException;

            if (httpException == null)
            {
                Log.Error(exception.Message, exception);
            }
            else
            {
                int statusCode = httpException.GetHttpCode();

                if (statusCode != (int)HttpStatusCode.NotFound && statusCode != (int)HttpStatusCode.ServiceUnavailable)
                {
                    Log.Error(exception.Message, httpException);
                }
            }
        }

        protected virtual void Application_BeginRequest(object sender, EventArgs e)
        {
            //Se Habilita para evitar error en las URLs q no contienen el "/" al final
            if (String.Compare(Request.Path, Request.ApplicationPath, StringComparison.InvariantCultureIgnoreCase) == 0
                && !(Request.Path.EndsWith("/")))
                Response.Redirect(Request.Path + "/");

            CultureInfo info = new CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.ToString());
            info.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            System.Threading.Thread.CurrentThread.CurrentCulture = info;
        }

        protected virtual void Application_EndRequest(object sender, EventArgs e)
        {

            HttpContext.Current?.ApplicationInstance?.Dispose();

            var context = (sender as HttpApplication).Context;

            if (context.Response.StatusCode == 401)
            {
                var noRedirect = context.Items["NoRedirect"];

                if (noRedirect == null)
                {
                    //var route = new RouteValueDictionary(new Dictionary<string, object>
                    //    {
                    //        { "Controller", "Account" },
                    //        { "Action", "SignIn" },
                    //        { "ReturnUrl", HttpUtility.UrlEncode(context.Request.RawUrl, context.Request.ContentEncoding) }
                    //    });

                    //Response.RedirectToRoute(route);
                }
            }
        }

        protected virtual void Application_End()
        {

            HttpRuntime runtime = (HttpRuntime)typeof(HttpRuntime).InvokeMember("_theRuntime",
                BindingFlags.NonPublic
                | BindingFlags.Static
                | BindingFlags.GetField,
                null,
                null,
                null);

            if (runtime == null)
                return;

            string shutDownMessage = (string)runtime.GetType().InvokeMember("_shutDownMessage",
                BindingFlags.NonPublic
                | BindingFlags.Instance
                | BindingFlags.GetField,
                null,
                runtime,
                null);

            string shutDownStack = (string)runtime.GetType().InvokeMember("_shutDownStack",
                BindingFlags.NonPublic
                | BindingFlags.Instance
                | BindingFlags.GetField,
                null,
                runtime,
                null);

            Log.Info(String.Format("\r\n\r\n_shutDownMessage={0}\r\n\r\n_shutDownStack={1}",
                shutDownMessage,
                shutDownStack));
        }

       
     }
}