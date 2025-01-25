using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Web.Xmarket.Models.Catalogo;
using static Utilitario.Common.ConstanteGeneral;

namespace Web.Xmarket.Controllers
{
    [AllowAnonymous]
    public class CatalogoController : Controller
    {
        private ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Server, NoStore = true)]

        public async Task<ActionResult> Index()
        {
            setMetadaHeader();

            CatalogoModel model = new CatalogoModel();
            return View(model);
        }

        private void setMetadaHeader()
        {
            ViewData[METADATA_WEB.TITULO] = $"Catalogo de Medicina | Limedica 🚚✅";
            ViewData[METADATA_WEB.DESCRIPCION] = $"Catalogo de Medicina 🚚✅";

            ViewData[METADATA_WEB.OG_TITULO] = $"Catalogo de Medicina | Limedica 🚚✅";
            ViewData[METADATA_WEB.OG_DESCRIPCION] = $"Catalogo de Medicina 🚚✅";

            ViewData[METADATA_WEB.OG_TYPE] = "website";
            ViewData[METADATA_WEB.OG_SITE_NAME] = "Literatura Medica EIRL - libros profesionales para la salud";
            ViewData[METADATA_WEB.OG_URL] = URL_WEB.LIMEDICA + "/catalogo";
            ViewData[METADATA_WEB.CANONICAL] = URL_WEB.LIMEDICA + "/catalogo";
        }
    }
}
