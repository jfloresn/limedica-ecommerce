using System.Web.Mvc;
using Web.Xmarket.ActionFilters;

namespace Web.Xmarket
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {

  
            filters.Add(new CustomAuthorizeAttribute(), 2);
            filters.Add(new SessionExpiredAttribute(), 3);
            filters.Add(new PerfilLoadAttribute(), 4);
            filters.Add(new EncryptedActionParameterAttribute(), 5);
            filters.Add(new GlobalViewDataAttribute(), 6);


        }
    }
}