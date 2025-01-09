using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Seguridad.Common;
using Web.Common;

namespace Web.Xmarket.Areas.MiCuenta.Controllers
{
    public class DashboardController : BaseController
    {
      
        public ActionResult Home()
        {
            return View();
        }

    }
}
