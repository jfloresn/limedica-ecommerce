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

    public string getCookie(HttpContext comext, string cookieName)
    {
        return comext.Request.Cookies[cookieName].Value;

    }

    public void updateCookie(Controller controller, string cookieName, string newValue, DateTime date)
    {

        updateCookies(controller.Request, controller.Response, cookieName, newValue, date);       



    }

    public void updateCookie(HttpContext context, string cookieName, string newValue, DateTime date)
    {
        HttpContextBase contextBase = new HttpContextWrapper(context);
        updateCookies(contextBase.Request, contextBase.Response, cookieName, newValue, date);



    }


    private void updateCookies(HttpRequestBase request, HttpResponseBase response, string cookieName, string newValue, DateTime date)
    {
        if (request.Cookies[cookieName] != null)
        {
            // Crea una cookie con el mismo nombre y una fecha de expiración pasada
            HttpCookie cookie = new HttpCookie(cookieName)
            {
                Expires = DateTime.Now.AddDays(-1) // Fecha de expiración pasada
            };

            // Añade la cookie al Response para eliminarla
            response.Cookies.Add(cookie);


        }


        HttpCookie httpCookie = new HttpCookie(cookieName, newValue);
        httpCookie.Expires = date;
        httpCookie.Secure = true;
        httpCookie.HttpOnly = true;
        response.Cookies.Add(httpCookie);

    }

}
