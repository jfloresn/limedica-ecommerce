using log4net;

using ServiceAgents.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Xmarket.Models.Home;
using Web.Common;
using QueryContracts.Xmarket.Especialidad.Parameters;
using QueryContracts.Xmarket.Especialidad.Result;
using QueryContracts.Xmarket.Editorial.Parameters;
using QueryContracts.Xmarket.Editorial.Result;
using QueryContracts.Xmarket.Book.Result;
using QueryContracts.Xmarket.Book.Parameters;
using QueryContracts.Xmarket.Book;
using Utilitario.Common;
using QueryContracts.Xmarket.Coleccion.Result;
using QueryContracts.Xmarket.Coleccion.Parameters;
using QueryContracts.Xmarket.Banner.Results;
using QueryContracts.Xmarket.Banner.Parameters;
using static Utilitario.Common.ConstanteGeneral;
using System.Threading.Tasks;
using StructureMap.Query;
using Web.Xmarket.DataAccess;
using CommandContracts.Xmarket.Sesion.Output;
using Seguridad.Common;
using System.Web.Http.Controllers;
using System.Runtime.Caching;
using Newtonsoft.Json;

namespace Web.Xmarket.Helpers.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {

        private ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

       

        [OutputCache(Duration = 600, VaryByParam = "none")]
        public async  Task<ActionResult> Index()
        {

            setMetadaHeader();

            HomeModel model = new HomeModel();
            try
            {
                // Iniciar todas las tareas asincrónicas en paralelo
                var bannersTask = new BannerListarParameter().ExecuteAsync();
                var editorialesTask = new ListarEditorialParameter().ExecuteAsync();
                var especialidadesTask = new ListarEspecialidadParameter().ExecuteAsync();


                // Esperar a que todas las tareas completen
                await Task.WhenAll(bannersTask, editorialesTask, especialidadesTask);

                // Obtener resultados de las tareas completadas
                model.banners = ((BannerListarResult)await bannersTask).Hits;
                model.editoriales = ((ListarEditorialResult)await editorialesTask).Hits;
                model.especialidades = ((ListarEspecialidadResult)await especialidadesTask).Hits;


                await updateCookiesSesionPublic();

            }
            catch (Exception err)
            {
                log.Error(err);
            }
        
            return  View(model);

        }

        private async Task updateCookiesSesionPublic()
        {

            await Task.Run(() =>
            {
                MemoryCache cache = MemoryCache.Default;
                var cachedItem = cache.Get("cache_sesion");

                if (cachedItem != null)
                {

                    string namewCookies = BaseCommon.Common.Comun.COOKIES_SESION_PUBLICO;

                    var sessionCache = (CrearSesionOutput)cachedItem;

                    var cookies = CookiesManager.Instance.getCookie(this, namewCookies);
                    Session sesionCookies = JsonConvert.DeserializeObject<Session>(cookies);

                    if (sesionCookies.CodSession <= 0)
                    {

                        sesionCookies.CodSession = sessionCache.CodigoSesion;

                        var timne = DateTime.UtcNow.AddMonths(3);

                        string valuecookies = JsonConvert.SerializeObject(sesionCookies);
                        CookiesManager.Instance.updateCookie(this, namewCookies, valuecookies, timne);
                    }
                }
            });




        }

        public async Task<PartialViewResult> obtenerProductSlide(string id)
        {

            IEnumerable<BookFiltroDTO> librosEbook = ((BookFiltroResult) await new BookFiltroParameter()
            {
                opcionFiltro = ConstanteGeneral.BOOK_FILTRO.FILTRO_POR_BOOK
            }
            .ExecuteAsync()).Hit;


            var model = new SWeb.Xmarket.Utilitario.Utilitarios().obtenerProductSlide(librosEbook);
            return PartialView("_libro_ebook_content", model);

        }

    

        public async Task<PartialViewResult> obtenerColeccionSlideAll()
        {

            IEnumerable<BookFiltroDTO> librosEbook = ((BookFiltroResult)await new BookFiltroParameter()
            {
                opcionFiltro = ConstanteGeneral.BOOK_FILTRO.FILTRO_POR_BOOK
            }
            .ExecuteAsync()).Hit;


            var model = new SWeb.Xmarket.Utilitario.Utilitarios().obtenerProductSlide(librosEbook);
            return PartialView("_libro_coleccion_content", model);

        }

        public async Task<PartialViewResult> obtenerNovedades(string id)
        {
            var model = ((BookFiltroResult)await new BookFiltroParameter() { opcionFiltro = ConstanteGeneral.BOOK_FILTRO.FILTRO_TODOS }.ExecuteAsync()).Hit;



            return PartialView("_libros_list", model);
        }
        public async Task<PartialViewResult> obtenerColeccionSlide(string id)
        {

            var colecciones =  ((ColeccionHomeResult) await new ColeccionHomeParameter() { }.ExecuteAsync()).Hit;

            var model = new SWeb.Xmarket.Utilitario.Utilitarios().obtenerColeccionSlide(colecciones);


            return PartialView("_libro_coleccion_content", model);


        }

        public async Task<PartialViewResult> obtenerEditorialesTodo(string id)
        {

            var model = ((ListarTodoEditorialResult)await new ListarTodoEditorialParameter().ExecuteAsync()).Hits;


            return PartialView("_editorial_content", model);


        }

        public async Task<PartialViewResult> obtenerProductoPorEspecialidad(string id)
        {
            var model = ((BookFiltroResult)new BookFiltroParameter()
            {
                opcionFiltro = ConstanteGeneral.BOOK_FILTRO.FILTRO_POR_ESPECIALIDAD,
                idEspecialdiad =Convert.ToInt32(id)
            }.Execute()).Hit;


            LibrosVerticalModel librosVertical = new LibrosVerticalModel();
            librosVertical.libros = model;
            librosVertical.tipofiltro = "especialidad";
            librosVertical.idEspecialidad = id;
            
            LeerEspecialidadResult especialidad = (LeerEspecialidadResult) await new LeerEspecialidadParameter() { idEspecialidad = Convert.ToInt32(id) }.ExecuteAsync();
            librosVertical.especialidad = especialidad.Hit.nombreUrl;


            return PartialView("_libros_vertical", librosVertical);
        }


        public async Task<PartialViewResult> obtenerProductoPorEditorial(string id)
        {
            var model = ((BookFiltroResult)new BookFiltroParameter()
            {
                opcionFiltro = ConstanteGeneral.BOOK_FILTRO.FILTRO_POR_EDITORIAL,
                idEditorial =Convert.ToInt32( id)
            }.Execute()).Hit;


            LibrosVerticalModel librosVertical = new LibrosVerticalModel();
            librosVertical.libros = model;
            librosVertical.tipofiltro = "editorial";
            librosVertical.idEditorial = id;

            LeerEditorialResult editorial = (LeerEditorialResult)await new LeerEditorialParameter() { idEditorial = Convert.ToInt32(id) }.ExecuteAsync();
            librosVertical.editorial = editorial.Hit.nombreUrl;

            return PartialView("_libros_vertical", librosVertical);
        }


        private void setMetadaHeader()
        {
            ViewData[METADATA_WEB.TITULO] = "Libros de medicina en Perú y a nivel nacional | Limedica 🚚✅";
            ViewData[METADATA_WEB.DESCRIPCION] = "Venta de libros de medicina y veterinaria de las distintas editoriales y especialidades a nivel nacional en Peru.";

            ViewData[METADATA_WEB.OG_TITULO] = "Libros de medicina en Perú y a nivel nacional | Limedica";
            ViewData[METADATA_WEB.OG_DESCRIPCION] = "Venta de libros de medicina y veterinaria de las distintas editoriales y especialidades a nivel nacional en Peru.";
            ViewData[METADATA_WEB.OG_TYPE] = "website";
            ViewData[METADATA_WEB.OG_SITE_NAME] = "Literatura Medica EIRL - libros profesionales para la salud";
            ViewData[METADATA_WEB.OG_URL] = URL_WEB.LIMEDICA;
            ViewData[METADATA_WEB.CANONICAL] = URL_WEB.LIMEDICA;
        }
    }

}
