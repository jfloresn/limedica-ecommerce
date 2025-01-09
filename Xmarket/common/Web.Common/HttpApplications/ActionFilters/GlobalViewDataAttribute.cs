namespace Web.Common.HttpApplications.ActionFilters
{
    using BaseCommon.Common;
    using global::Seguridad.Common;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class GlobalViewDataAttribute : ActionFilterAttribute
    {

  
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            if (!filterContext.IsChildAction && !filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var controller = filterContext.Controller as BaseController;
                if (controller != null)
                {
                    var viewBag = controller.ViewBag;
                    Session session = controller.Session;
                    if (session!=null)
                    {

                        viewBag.Usuario = session.Usuario;
                      
                    }
                  
                }
            }





        }
    }
}
