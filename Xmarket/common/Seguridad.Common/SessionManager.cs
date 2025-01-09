namespace Seguridad.Common
{
    using BaseCommon.Common;
    using log4net;
 
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using QueryContracts.Xmarket.Cliente.Result;
    using System;
    using System.Net;
    using System.Reflection;
    using System.Runtime.Remoting.Contexts;
    using System.Threading.Tasks;
    using System.Web;

    public class SessionManager
    {
        private readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);




        public SessionManager()
        {
        }

        private static readonly Lazy<SessionManager> _instance =
        new Lazy<SessionManager>(() => new SessionManager());

        public static SessionManager Instance
        {
            get { return _instance.Value; }
        }




        public void addCookies(CookiesType cookies)
        {
         

    

            HttpCookie cookie = new HttpCookie(cookies.codigo_cookies, cookies.valor_cookies);
            cookie.Expires = DateTime.Now.AddMonths(3);

            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        }

   

        public void crearSesionDeSesionPublica(ClienteLeerCorreoResult resultado)
        {
           var sessionPublica = SessionWrapper.Instance.getSesionPublico();

            CookiesType cookiesUsuario = new CookiesType();
            Session session = new Session();
            session.CodSession = sessionPublica.CodSession;
            session.CodSessionPulbico = sessionPublica.CodSessionPulbico;
            session.FechaInicio = DateTime.Now;
            session.IpCliente = new BaseCommon.Common.ClienteWeb().ObtenerIpPublic();
            session.Usuario = new Usuario();
            session.autenticado = true;
            session.Usuario.NombreUsuario = resultado.Hit.nombre;
            session.Usuario.ApellidoPaterno = resultado.Hit.apellidoPaterno;
            session.Usuario.ApellidoMaterno = resultado.Hit.apellidoMaterno;
            session.Usuario.EmailUsuario = resultado.Hit.correo;
            session.Usuario.Idusuario = resultado.Hit.IdUsuario;

            cookiesUsuario.codigo_cookies = BaseCommon.Common.Comun.COOKIES_SESION;
            cookiesUsuario.fechaExpira = DateTime.Now.AddMonths(1);
            cookiesUsuario.valor_cookies = JsonConvert.SerializeObject(session);

            this.addCookies(cookiesUsuario);


        }

        public void crearSesionPublico(Session _sesion)
        {
            CookiesType cookiesUsuario = new CookiesType();
            cookiesUsuario.codigo_cookies = BaseCommon.Common.Comun.COOKIES_SESION_PUBLICO;
            cookiesUsuario.fechaExpira = DateTime.Now.AddMonths(1);
            cookiesUsuario.valor_cookies = JsonConvert.SerializeObject(_sesion);
            this.addCookies(cookiesUsuario);
        }

        public void addCantidadCarrito(SesionCarrito sesionCarrito)
        {
            if (string.IsNullOrEmpty(this.getCookies(BaseCommon.Common.Comun.COOKIES_SESION_CANTIDAD_CARRITO)))
            {
                this.deleteCookies(this.getCookies(BaseCommon.Common.Comun.COOKIES_SESION_CANTIDAD_CARRITO));
            }

            CookiesType cookiesUsuario = new CookiesType();
            cookiesUsuario.codigo_cookies = BaseCommon.Common.Comun.COOKIES_SESION_CANTIDAD_CARRITO;
            cookiesUsuario.fechaExpira = DateTime.Now.AddMonths(1);
            cookiesUsuario.valor_cookies = JsonConvert.SerializeObject(sesionCarrito);

            this.addCookies(cookiesUsuario);

        }

        public void crearCookiesCarritoCodigo(int _codigoPedido)
        {
            CookiesType cookiesUsuario = new CookiesType();
            cookiesUsuario.codigo_cookies = BaseCommon.Common.Comun.COOKIES_CARRITO_CODIGO;
            cookiesUsuario.fechaExpira = DateTime.Now.AddMonths(1);
            cookiesUsuario.valor_cookies = JsonConvert.SerializeObject(_codigoPedido);
            this.addCookies(cookiesUsuario);
        }

        public SesionCarrito getsesionCarritoCantidad()
        {

            return JsonConvert.DeserializeObject<SesionCarrito>(this.getCookies(BaseCommon.Common.Comun.COOKIES_SESION_CANTIDAD_CARRITO));


        }

        public void deleteCookies(string id)
        {
           

     
            HttpCookie cookie = new HttpCookie(id);
            cookie.Expires = DateTime.UtcNow.AddDays(-1);

            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);

        }

        public string getCookies(string cookieKey)
        {
            string result = "";
            try
            {
                if (System.Web.HttpContext.Current.Request.Cookies.Get(cookieKey) != null)
                {

                    result = System.Web.HttpContext.Current.Request.Cookies.Get(cookieKey).Value;
                }
               
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        
            return result;

        }

      

    }
}