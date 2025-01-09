using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using log4net;


public class CookiesManager
{
    private ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    private static readonly Lazy<CookiesManager> _instance =
     new Lazy<CookiesManager>(() => new CookiesManager());

    public static CookiesManager Instance
    {
        get { return _instance.Value; }
    }


    public CookiesManager()
    {

    }

    public void registerCookies(Controller controller, string cookieName, string newValue, DateTime date)
    {
        HttpCookie httpCookie = new HttpCookie(cookieName, newValue);
        httpCookie.Expires = date;
        httpCookie.Secure = true;
        httpCookie.HttpOnly = true;
        controller.HttpContext.Response.Cookies.Add(httpCookie);
    }

    public string getCookie(Controller controller, string cookieName)
    {
        return controller.HttpContext.Request.Cookies[cookieName].Value;

    }

    public void updateCookie(Controller controller, string cookieName, string newValue, DateTime date)
    {

        if (controller.Request.Cookies[cookieName] != null)
        {
            // Crea una cookie con el mismo nombre y una fecha de expiración pasada
            HttpCookie cookie = new HttpCookie(cookieName)
            {
                Expires = DateTime.Now.AddDays(-1) // Fecha de expiración pasada
            };

            // Añade la cookie al Response para eliminarla
            controller.Response.Cookies.Add(cookie);


        }


        HttpCookie httpCookie = new HttpCookie(cookieName, newValue);
        httpCookie.Expires = date;
        httpCookie.Secure = true;
        httpCookie.HttpOnly = true;
        controller.HttpContext.Response.Cookies.Add(httpCookie);


    }

}
