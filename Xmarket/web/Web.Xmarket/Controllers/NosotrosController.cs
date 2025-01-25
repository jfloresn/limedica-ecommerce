using log4net;
using QueryContracts.Xmarket.Banner;
using QueryContracts.Xmarket.Banner.Parameters;
using QueryContracts.Xmarket.Banner.Results;
using QueryContracts.Xmarket.Categoria;
using QueryContracts.Xmarket.Categoria.Parameters;
using QueryContracts.Xmarket.Categoria.Result;
using QueryContracts.Xmarket.Marca;
using QueryContracts.Xmarket.Marca.Parameters;
using QueryContracts.Xmarket.Marca.Result;
using ServiceAgents.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Xmarket.Utilitario;
using Web.Xmarket.Models.Home;
using Web.Common;
using Seguridad.Common;
using QueryContracts.Xmarket.Carrito.Result;
using Web.Xmarket.DataAccess.Carrito;
using Web.Xmarket.Models.Nosotros;
using QueryContracts.Xmarket.Editorial.Result;
using QueryContracts.Xmarket.Editorial.Parameters;
using static Utilitario.Common.ConstanteGeneral;
using System.Threading.Tasks;
using System.Web.UI;

namespace Web.Xmarket.Helpers.Controllers
{
    [AllowAnonymous]
    public class NosotrosController : BaseController
    {

        private ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);



 

        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Server, NoStore = true)]

        public async Task<ActionResult> Index()
        {
            setMetadaHeader();

            NosotrosModel model = new NosotrosModel();

            model.editoriales = ((ListarTodoEditorialResult)await new ListarTodoEditorialParameter().ExecuteAsync()).Hits;


            return View(model);
        }



        private void setMetadaHeader()
        {
            ViewData[METADATA_WEB.TITULO] = $"Nosotros | Limedica 🚚✅";
            ViewData[METADATA_WEB.DESCRIPCION] = $"Nosotros 🚚✅";

            ViewData[METADATA_WEB.OG_TITULO] = $"Nosotros | Limedica 🚚✅";
            ViewData[METADATA_WEB.OG_DESCRIPCION] = $"Nosotros 🚚✅";

            ViewData[METADATA_WEB.OG_TYPE] = "website";
            ViewData[METADATA_WEB.OG_SITE_NAME] = "Literatura Medica EIRL - libros profesionales para la salud";
            ViewData[METADATA_WEB.OG_URL] = URL_WEB.LIMEDICA + "/nosotros";
            ViewData[METADATA_WEB.CANONICAL] = URL_WEB.LIMEDICA + "/nosotros";
        }

    }
}
