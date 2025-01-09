using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace Web.Common.HtmlHelpers
{
    public class SessionHelper
    {
        private ILog _lloger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        private static readonly Lazy<SessionHelper> _instance =
        new Lazy<SessionHelper>(() => new SessionHelper());

        public static SessionHelper Instance
        {
            get { return _instance.Value; }
        }


        public void DestroyUserSession()
        {
            FormsAuthentication.SignOut();
        }
        public  int GetUser()
        {
            int user_id = 0;
            try
            {
                var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

                if (cookie != null && !string.IsNullOrWhiteSpace(cookie.Value))
                {
                    var ticket = FormsAuthentication.Decrypt(cookie.Value);
                    user_id = ticket.UserData == null ? 0 : Convert.ToInt32(ticket.UserData);
                }
            }
            catch (Exception err) {
                _lloger.Error("GetUser", err);
            }
            return user_id;
        }

        public  void AddUserToSession(string id)
        {
            bool persist = true;
            var cookie = FormsAuthentication.GetAuthCookie("usuario", persist);

            cookie.Name = FormsAuthentication.FormsCookieName;
            cookie.Expires = DateTime.Now.AddMonths(3);

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
          
            var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, DateTime.Now.AddMonths(3), ticket.IsPersistent, id);

            cookie.Value = FormsAuthentication.Encrypt(newTicket);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}