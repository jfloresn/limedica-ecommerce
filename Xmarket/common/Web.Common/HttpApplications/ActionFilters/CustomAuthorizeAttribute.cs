using System.Web.Mvc;
using System.Web.Routing;
using Utilitario.Common;
using Web.Common.HtmlHelpers;

namespace Web.Common.HttpApplications.ActionFilters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            var user = SessionHelper.Instance.GetUser();

            
            if (filterContext.Result is HttpUnauthorizedResult && filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Items["RequestWasNotAuthorized"] = true;
            }

            if ( ( filterContext.HttpContext.Request.Url.AbsolutePath.ToLower().Equals(ConstanteGeneral.PATH_CUENTA.DETALLE_CUENTA) ||
                  filterContext.HttpContext.Request.Url.AbsolutePath.ToLower().Equals(ConstanteGeneral.PATH_CUENTA.MICUENTA) ||
                  filterContext.HttpContext.Request.Url.AbsolutePath.ToLower().Equals(ConstanteGeneral.PATH_CUENTA.DIRECCIONES) ||
                  filterContext.HttpContext.Request.Url.AbsolutePath.ToLower().Equals(ConstanteGeneral.PATH_CUENTA.ORDENES) ||
                  filterContext.HttpContext.Request.Url.AbsolutePath.ToLower().Equals(ConstanteGeneral.PATH_CUENTA.MEOTOD_PAGO)
                  ) &&
                user<=0 ) {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Cuenta",
                    action = "Index"
                }));
            }


        }
    }
}
