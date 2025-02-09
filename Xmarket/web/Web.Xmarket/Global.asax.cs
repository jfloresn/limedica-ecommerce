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
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Windows.Input;
    using Web.Common.HttpApplications;
    using Web.Xmarket.DataAccess;
    using Web.Xmarket.Security;
 
    public class MvcApplication : CommonHttpApplication
    {
        private readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        private static int _usuariosConectados = 0;

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
            Interlocked.Increment(ref _usuariosConectados);


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
            session.idSesionWeb = sesionWeb;
            session.idVerificacionToken= sesionVericationtoekn;


            Session.Add(BaseCommon.Common.Comun.COOKIES_SESION_PUBLICO, session);


            registerCookies(session);
            createAndUpdateCacheSesion(session);
            crearSesion(session);
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

        private void createAndUpdateCacheSesion(Session session)
        {
            string idSession = session.CodSessionPulbico;

            CatalagoManager.Instance.saveCache(session, idSession);
        }

        private Session getCacheSesion(Session session)
        {
            string idSession = session.CodSessionPulbico;
            string keyMap = $"{idSession}";
            var cacheSesion = (Session)CatalagoManager.Instance.getCache(keyMap);

            return cacheSesion;


        }

        private void crearSesion(Session session)
        {

            Task.Run(async () =>
            {
                try
                {
                    CrearSesionCommand sesionComman = new CrearSesionCommand();

                    sesionComman.codigoSesionPublico = session.CodSessionPulbico;
                    sesionComman.FechaInicio = session.FechaInicio;
                    sesionComman.IpPublica = session.IpCliente;
                    sesionComman.idSessionWeb = session.idSesionWeb;
                    sesionComman.requestVerificationToken = session.idVerificacionToken;

                    var sesion = (CrearSesionOutput)await sesionComman.ExecuteAsync();

                    var cacheSesion = getCacheSesion(session);
                    cacheSesion.CodSession = sesion.CodigoSesion;
                    createAndUpdateCacheSesion(session);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            });
        }

        protected virtual void Session_End(Object sender, EventArgs e)
        {
            Interlocked.Decrement(ref _usuariosConectados);

            MemoryCache cache = MemoryCache.Default;
            var cachedItem = cache.Get("cache_sesion");

            // este metodo es adecuado para elikminar datos de cache


        }

        public static int ObtenerUsuariosConectados()
        {
            return _usuariosConectados;
        }
    }
}