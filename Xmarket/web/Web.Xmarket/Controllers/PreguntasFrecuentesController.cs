using log4net;
using System.Reflection;
using System.Web.Mvc;
using Web.Xmarket.Models.Home;
using Web.Common;
using static Utilitario.Common.ConstanteGeneral;
using System.Threading.Tasks;
using Seguridad.Common;
using System.Web.UI;

namespace Web.Xmarket.Helpers.Controllers
{
    [AllowAnonymous]
    public class PreguntasFrecuentesController : BaseController
    {

        private ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);



        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Server, NoStore = true)]

        public async Task<ActionResult> Index()
        {
            setMetadaHeader();

            HomeModel model = new HomeModel();
            return View(model);
        }

        private void setMetadaHeader()
        {
            ViewData[METADATA_WEB.TITULO] = $"Preguntas Frecuentes | Limedica 🚚✅";
            ViewData[METADATA_WEB.DESCRIPCION] = $"Preguntas Frecuentes 🚚✅";

            ViewData[METADATA_WEB.OG_TITULO] = $"Preguntas Frecuentes | Limedica 🚚✅";
            ViewData[METADATA_WEB.OG_DESCRIPCION] = $"Preguntas Frecuentes 🚚✅";

            ViewData[METADATA_WEB.OG_TYPE] = "website";
            ViewData[METADATA_WEB.OG_SITE_NAME] = "Literatura Medica EIRL - libros profesionales para la salud";
            ViewData[METADATA_WEB.OG_URL] = URL_WEB.LIMEDICA + "/preguntas-frecuentes";
            ViewData[METADATA_WEB.CANONICAL] = URL_WEB.LIMEDICA + "/preguntas-frecuentes";
        }
    }
}
