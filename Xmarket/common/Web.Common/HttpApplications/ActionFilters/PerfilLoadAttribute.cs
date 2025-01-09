namespace Web.Common.HttpApplications.ActionFilters
{
    using global::Seguridad.Common;
    using System;
    using System.Web;
    using System.Web.Mvc;
    using Web.Common.AuthenticationServices;
    using Web.Common.HtmlHelpers;

    public class PerfilLoadAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           
           
        }
    }
}