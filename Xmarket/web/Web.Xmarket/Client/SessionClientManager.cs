

using System.Reflection;
using System;
using System.Web;
using System.Web.Mvc;
using log4net;
using Newtonsoft.Json;
using Seguridad.Common;
using Web.Xmarket.DataAccess;
using System.Web.Http.Controllers;
using RazorEngine.Compilation.ImpromptuInterface.Dynamic;
using RazorEngine.Compilation.ImpromptuInterface.InvokeExt;

public class SessionClientManager
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


    public void updatecookiesSesionPublic()
    {
        var current = HttpContext.Current;

        var timeMonthTree = DateTime.UtcNow.AddMonths(3);

        string namewCookies = BaseCommon.Common.Comun.COOKIES_SESION_PUBLICO;
        var cookies = CookiesManager.Instance.getCookie(current, namewCookies);
        Session cookiesSesion = JsonConvert.DeserializeObject<Session>(cookies);

        string idSessionPublico = cookiesSesion.CodSessionPulbico;
        string keyMap = $"{idSessionPublico}";
        var cacheSesion = (Session)CatalagoManager.Instance.getCache(keyMap);

        if (cacheSesion != null)
        {
            if (cookiesSesion.CodSession <= 0)
            {
                cookiesSesion.CodSession = cacheSesion.CodSession;

                string cookiesSessionJson = JsonConvert.SerializeObject(cookiesSesion);
                CookiesManager.Instance.updateCookie(current, namewCookies, cookiesSessionJson, timeMonthTree);
            }

        }
    }



}