

namespace Web.Xmarket
{
    using BaseCommon.Common;
    using CommandContracts.Xmarket.Sesion;
    using CommandContracts.Xmarket.Sesion.Output;
    using global::Seguridad.Common;
    using log4net;
    using Newtonsoft.Json;
    using ServiceAgents.Common;
    using System;
    using System.Reflection;
    using System.Runtime.Caching;
    using System.Threading.Tasks;
    using System.Web;
    using Web.Common.HttpApplications;
    using Web.Xmarket.DataAccess;
    using Web.Xmarket.Security;
 


    public class MvcApplication : CommonHttpApplication
    {
        private readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

      

        protected virtual void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session == null) return;
            if (!this.Request.IsAuthenticated) return;
            var identity = new CustomIdentity(HttpContext.Current.User.Identity);
            var principal = new CustomPrincipal(identity);
            HttpContext.Current.User = principal;

        }


        protected virtual void Session_Start(Object sender, EventArgs e)
        {
            String sessionPublico = AppConfigWeb.serverName + new BaseCommon.Common.ClienteWeb().getCodigoSesionPublico();
            String ipPublica = new BaseCommon.Common.ClienteWeb().ObtenerIpPublic();
            String sesionWeb = getSessionWebId();
            string sesionVericationtoekn = getSessionVerificationToken();

            Session session = new Session();
            session.CodSession = 0;
            session.CodSessionPulbico = sessionPublico;
            session.autenticado = false;
            session.FechaInicio = DateTime.Now;
            session.IpCliente = ipPublica;

            registerCookies(session);
            crearSesion(sessionPublico, ipPublica, sesionWeb, sesionVericationtoekn);
        }

        private void registerCookies(Session _sesion)
        {

            HttpCookie cookiesUsuario = new HttpCookie(BaseCommon.Common.Comun.COOKIES_SESION_PUBLICO);
            cookiesUsuario.Expires = DateTime.Now.AddMonths(3);
            cookiesUsuario.Value = JsonConvert.SerializeObject(_sesion);
            HttpContext.Current.Response.Cookies.Add(cookiesUsuario);
        }

        private string getSessionWebId()
        {
            string result ="";
            if (HttpContext.Current.Request.Cookies[BaseCommon.Common.Comun.SESSION_WEB_ID] != null)
            {
                result = HttpContext.Current.Request.Cookies[BaseCommon.Common.Comun.SESSION_WEB_ID].Value;
            }


            return result;
        }

        private string getSessionVerificationToken()
        {
            string result = "";
            if (HttpContext.Current.Request.Cookies[BaseCommon.Common.Comun.SESSION_REQUEST_VERIFICATION_TOKEN] != null)
            {
                result = HttpContext.Current.Request.Cookies[BaseCommon.Common.Comun.SESSION_REQUEST_VERIFICATION_TOKEN].Value;
            }


            return result;

        }

        private void crearSesion(String sessionPublico, String ip, string sesionWeb, string SesionVerifcation)
        {

            Task.Run(async () =>
            {
                try
                {
                    CrearSesionCommand sesionComman = new CrearSesionCommand();

                    sesionComman.codigoSesionPublico = sessionPublico;
                    sesionComman.FechaInicio = DateTime.Now;
                    sesionComman.IpPublica = ip;
                    sesionComman.idSessionWeb = sesionWeb;
                    sesionComman.requestVerificationToken = SesionVerifcation;

                    var sesion = (CrearSesionOutput) await sesionComman.ExecuteAsync();

                 
                   // registrar en cache
                   CatalagoManager.Instance.saveCache(sesion, "cache_sesion");

                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            });

        }

        protected virtual void Session_End(Object sender, EventArgs e)
        {

            MemoryCache cache = MemoryCache.Default;
            var cachedItem = cache.Get("cache_sesion");

            // este metodo es adecuado para elikminar datos de cache


        }
    }
}