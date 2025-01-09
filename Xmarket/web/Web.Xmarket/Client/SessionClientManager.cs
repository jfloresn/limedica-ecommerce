

using System.Reflection;
using System;
using System.Web;
using System.Web.Mvc;
using log4net;
using Newtonsoft.Json;
using Seguridad.Common;

partial class SessionClientManager
{

    private ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    private static readonly Lazy<SessionClientManager> _instance =
     new Lazy<SessionClientManager>(() => new SessionClientManager());

    public static SessionClientManager Instance
    {
        get { return _instance.Value; }
    }


    public SessionClientManager()
    {

    }
    public SesionCarrito getSesionCarritoCantidad()
    {
        string cookiesCarritoCantidad = HttpContext.Current.Request.Cookies[BaseCommon.Common.Comun.COOKIES_SESION_CANTIDAD_CARRITO].Value;
        return JsonConvert.DeserializeObject<SesionCarrito>(cookiesCarritoCantidad);


    }




}