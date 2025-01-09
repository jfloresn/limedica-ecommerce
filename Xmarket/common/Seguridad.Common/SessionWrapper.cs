using System.Web;
using log4net;              
using System.Reflection;
using System;
using BaseCommon.Common;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;

namespace Seguridad.Common

{
    public class SessionWrapper
    {

        private ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly Lazy<SessionWrapper> _instance =
         new Lazy<SessionWrapper>(() => new SessionWrapper(new SessionManager()));


        private  readonly SessionManager _sessionManager;

        public SessionWrapper(SessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

      

        public static SessionWrapper Instance
        {
            get { return _instance.Value; }
        }

        public Session getSesionPublico()
        {
          
            Session session = JsonConvert.DeserializeObject<Session>(_sessionManager.getCookies(BaseCommon.Common.Comun.COOKIES_SESION_PUBLICO));
            return session;
        }

        public async Task<Session> getSesionPublicoAsync()
        {


            return await Task.Run(() =>
            {

              
                Session session = JsonConvert.DeserializeObject<Session>(_sessionManager.getCookies(BaseCommon.Common.Comun.COOKIES_SESION_PUBLICO));

                return session;

            });

        }

        public void eliminarCantidadCarrito()
        {
            SessionManager sesionManager = new SessionManager();
            sesionManager.deleteCookies(BaseCommon.Common.Comun.COOKIES_SESION_CANTIDAD_CARRITO);
        }

        public void setCookiesSesionPublico(long idUsuarioLibre)
        {
           
                string valor = HttpContext.Current.Request.Cookies[BaseCommon.Common.Comun.COOKIES_SESION_PUBLICO]?.Value;


                var cookies =  _sessionManager.getCookies(BaseCommon.Common.Comun.COOKIES_SESION_PUBLICO);
                Session session = JsonConvert.DeserializeObject<Session>(cookies);
                if (!string.IsNullOrEmpty(_sessionManager.getCookies(BaseCommon.Common.Comun.COOKIES_SESION_PUBLICO)))
                {
                    _sessionManager.deleteCookies(_sessionManager.getCookies(BaseCommon.Common.Comun.COOKIES_SESION_PUBLICO));
                }
                session.idUsuarioLibre = idUsuarioLibre;
                updateCookiesSesionPublico(session);
         


        }


        private void updateCookiesSesionPublico(Session sesion)
        {
           
            CookiesType cookiesUsuario = new CookiesType();
            cookiesUsuario.codigo_cookies = BaseCommon.Common.Comun.COOKIES_SESION_PUBLICO;
            cookiesUsuario.fechaExpira = DateTime.Now.AddMonths(3);
            cookiesUsuario.valor_cookies = JsonConvert.SerializeObject(sesion);
            _sessionManager.addCookies(cookiesUsuario);
        }

        public void eliminarCarrito()
        {


            _sessionManager.deleteCookies(BaseCommon.Common.Comun.COOKIES_CARRITO_CODIGO);
        }


        public void eliminarUsuarioLibre()
        {
         
            Session session = JsonConvert.DeserializeObject<Session>(_sessionManager.getCookies(BaseCommon.Common.Comun.COOKIES_SESION_PUBLICO));
            if (!string.IsNullOrEmpty(_sessionManager.getCookies(BaseCommon.Common.Comun.COOKIES_SESION_PUBLICO)))
            {
                _sessionManager.deleteCookies(BaseCommon.Common.Comun.COOKIES_SESION_PUBLICO);
            }

            session.idUsuarioLibre = 0;
            CookiesType cookiesUsuario = new CookiesType();
            cookiesUsuario.codigo_cookies = BaseCommon.Common.Comun.COOKIES_SESION_PUBLICO;
            cookiesUsuario.fechaExpira = DateTime.Now.AddMonths(3);
            cookiesUsuario.valor_cookies = JsonConvert.SerializeObject(session);
            _sessionManager.addCookies(cookiesUsuario);
        }

    

     

        public Session Session
        {
            get
            {
                if (HttpContext.Current.Request.Cookies[Comun.COOKIES_SESION] != null)
                {
                    try
                    {
                       
                        Session session = JsonConvert.DeserializeObject<Session>(HttpContext.Current.Request.Cookies[BaseCommon.Common.Comun.COOKIES_SESION].Value);
                        return session;
                    }
                    catch (Exception err)
                    {

                        log.Error("SessionWrapper properties Session", err);
                    }
                }
                return null;
            }

        }


        public Usuario Usuario
        {
            get
            {
                if (HttpContext.Current.Request.Cookies[Comun.COOKIES_SESION] != null)
                {


                 

                    Session session = JsonConvert.DeserializeObject<Session>(HttpContext.Current.Request.Cookies[BaseCommon.Common.Comun.COOKIES_SESION].Value);

                    return session.Usuario;
                }
                return null;
            }
        }

        public Nullable<int> getCodigoCarritoCodigo()
        {

            int? cokies = null;

            if (!string.IsNullOrEmpty(_sessionManager.getCookies(BaseCommon.Common.Comun.COOKIES_CARRITO_CODIGO)))
            {
                cokies = Convert.ToInt32(_sessionManager.getCookies(BaseCommon.Common.Comun.COOKIES_CARRITO_CODIGO));
            }
            return cokies;

        }

    }
}
