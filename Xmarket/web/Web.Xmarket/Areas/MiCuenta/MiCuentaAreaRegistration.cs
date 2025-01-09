using System.Web.Mvc;

namespace Web.Xmarket.Areas.MiCuenta
{
    public class MiCuentaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MiCuenta";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MiCuenta_default",
                "MiCuenta/{controller}/{action}/{id}",
                new { action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}
