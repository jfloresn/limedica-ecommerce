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
using Web.Xmarket.Models.Coleccion;
using QueryContracts.Xmarket.Coleccion.Result;
using QueryContracts.Xmarket.Coleccion.Parameters;
using QueryContracts.Xmarket.Book.Result;
using QueryContracts.Xmarket.Book.Parameters;
using Utilitario.Common;
using static Utilitario.Common.ConstanteGeneral;
using System.Threading.Tasks;

namespace Web.Xmarket.Helpers.Controllers
{
    [AllowAnonymous]
    public class ColeccionesController : BaseController
    {

        private ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

     

        public async Task<ActionResult> Index()
        {
            setMetadaHeader();

            ColeccionModel model = new ColeccionModel();

            model.colecciones = ((ColeccionListarResult)await new ColeccionListarParameter().ExecuteAsync()).Hit;

            return View(model);
        }


        public async Task<ActionResult> libros(string nombre, string id)
        {

            setMetadaHeaderLibros(nombre, Url.RouteUrl("ColeccionPorId", new { nombre = nombre, id = id }));

            ColeccionDetalleModel model = new ColeccionDetalleModel();


            try {

             
                model.request = new ObtenerLibroColeccionRequest();

                model.request = new ObtenerLibroColeccionRequest();
                model.request.idColeccion =Convert.ToInt32(id == null?"0": id);
                model.request.coleccionNombre = nombre;

                model.bookListarResult = (BookListarResult)await new BookListarParameter()
                {
                    opcionFiltro = ConstanteGeneral.BOOK_FILTRO.FILTRO_POR_COLECCION,
                    idColeccion = Convert.ToInt32(model.request.idColeccion),
                    registroInicio = 0,
                    registroFin = ParametrosConfigurado.COMPAGINADO
                }.ExecuteAsync();


                model.coleccionResult = (LeerColeccionResult)await new LeerColeccionParameter()
                {
                    idColeccion = model.request.idColeccion
                }.ExecuteAsync();


            }
            catch (Exception err) {

                model = new ColeccionDetalleModel();

                log.Error(err);
            }
            finally { 
            
            }

         
            return View(model);
        }

        public async Task<PartialViewResult> obtenerLibrosCompaginados(ObtenerLibroColeccionRequest request)
        {
            ColeccionDetalleModel model = new ColeccionDetalleModel();

                model.bookListarResult = (BookListarResult)await new BookListarParameter()
                {
                    opcionFiltro = ConstanteGeneral.BOOK_FILTRO.FILTRO_POR_COLECCION,
                    idColeccion = request.idColeccion,
                    registroInicio = request.cantidadRegistroInicio + 1,
                    registroFin = (request.cantidadRegistroInicio + 1) + (request.cantidadRegistroCompaginado - 1)
                }.ExecuteAsync();
            return PartialView("_libros_list", model.bookListarResult.Hit);
        }

        private void setMetadaHeader()
        {
            ViewData[METADATA_WEB.TITULO] = $"Colección de Medicina | Limedica 🚚✅";
            ViewData[METADATA_WEB.DESCRIPCION] = $"Colección de Medicina";

            ViewData[METADATA_WEB.OG_TITULO] = $"Colección de Medicina | Limedica";
            ViewData[METADATA_WEB.OG_DESCRIPCION] = $"Colección de Medicina";

            ViewData[METADATA_WEB.OG_TYPE] = "website";
            ViewData[METADATA_WEB.OG_SITE_NAME] = "Literatura Medica EIRL - libros profesionales para la salud";
            ViewData[METADATA_WEB.OG_URL] = URL_WEB.LIMEDICA + "/coleccion";
            ViewData[METADATA_WEB.CANONICAL] = URL_WEB.LIMEDICA + "/coleccion";
        }

        private void setMetadaHeaderLibros(string coleccion, string urlColeccion)
        {
            ViewData[METADATA_WEB.TITULO] = $"Colección {coleccion} | Limedica 🚚✅";
            ViewData[METADATA_WEB.DESCRIPCION] = $"Colección {coleccion}";

            ViewData[METADATA_WEB.OG_TITULO] = $"Colección de {coleccion} | Limedica";
            ViewData[METADATA_WEB.OG_DESCRIPCION] = $"Colección de Medicina";

            ViewData[METADATA_WEB.OG_TYPE] = "website";
            ViewData[METADATA_WEB.OG_SITE_NAME] = "Literatura Medica EIRL - libros profesionales para la salud";
            ViewData[METADATA_WEB.OG_URL] = URL_WEB.LIMEDICA + urlColeccion ;
            ViewData[METADATA_WEB.CANONICAL] = URL_WEB.LIMEDICA +  urlColeccion ;
        }
    }
}
