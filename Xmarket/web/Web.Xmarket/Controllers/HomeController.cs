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
using System.Security.RightsManagement;
using Web.Xmarket.Models.Catalogo;
using QueryContracts.Xmarket.Editorial;
using QueryContracts.Xmarket.Especialidad;
using QueryContracts.Xmarket.Banner;
using  System.Web.SessionState;
using System.Reactive.Linq;
using Org.BouncyCastle.Crypto.Modes;
using System.Reactive.Threading.Tasks;
using System.Web.UI;

namespace Web.Xmarket.Helpers.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {

        private ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Server, NoStore = true)]
        public async Task<ActionResult> Index()
        {
            setMetadaHeader();

            try
            {
                var model = await getBannerEditoralEspecial(new HomeModel()).ConfigureAwait(false);
                return View(model);
            }
            catch (ArgumentException argEx)
            {
                log.Warn("Parámetro inválido en Index", argEx);
            }
            catch (TimeoutException timeoutEx)
            {
                log.Warn("Tiempo de espera agotado en Index", timeoutEx);
            }
            catch (Exception ex)
            {
                log.Error("Error inesperado en Index", ex);
            }

            return View(new HomeModel()); // Devuelve un modelo vacío en caso de error
        }

        private async Task<HomeModel> getBannerEditoralEspecial(HomeModel model)
        {
        
            string keyMapEspecialidad = $"{BaseCommon.Common.Comun.CACHE_ESPECIALIDAD}";
            string keyMapBanner = $"{BaseCommon.Common.Comun.CACHE_BANNER}";
            string keyMapEditorial = $"{BaseCommon.Common.Comun.CACHE_EDITORIAL}";

            var bannersTask = getBanner(keyMapBanner);
            var especialidadesTask = getEspecialidad(keyMapEspecialidad);
            var editorialesTask = getEditorial(keyMapEditorial);

            await Task.WhenAll(bannersTask, especialidadesTask, editorialesTask).ConfigureAwait(false);

            model.banners = bannersTask.Result;
            model.editoriales = editorialesTask.Result;
            model.especialidades = especialidadesTask.Result;

            return model;
        }

   

        public async Task<IEnumerable<BannerDTO>> getBanner(string keyMap)
        {
            var cacheBanner = (IEnumerable<BannerDTO>)CatalagoManager.Instance.getCache(keyMap);

            if (cacheBanner != null && cacheBanner.Any())
            {

                log.Info("lectura de banner desde la cache ");
                return cacheBanner;
            }
            log.Info("lectura de banner desde la base de datos ");
            var bannerTask = await new BannerListarParameter().ExecuteAsync();
            var bannerBase = ((BannerListarResult)bannerTask).Hits;

            saveCacheBanner(bannerBase, keyMap);

            return bannerBase;
        }


        public async Task<IEnumerable<EspecialidadDTO>> getEspecialidad(string keyMap)
        {
            var cacheEspecialidad = (IEnumerable<EspecialidadDTO>)CatalagoManager.Instance.getCache(keyMap);

            if (cacheEspecialidad != null && cacheEspecialidad.Any())
            {
                return cacheEspecialidad;
            }

            var especialidadTask = await new ListarEspecialidadParameter().ExecuteAsync();
            var especialidadBase = ((ListarEspecialidadResult)especialidadTask).Hits;

            saveCacheEspecialidad(especialidadBase, keyMap);

            return especialidadBase;
        }

        public async Task<IEnumerable<EditorialDTO>> getEditorial(string keyMap)
        {
            var cacheEditorial = (IEnumerable<EditorialDTO>)CatalagoManager.Instance.getCache(keyMap);

            if (cacheEditorial != null && cacheEditorial.Any())
            {
                return cacheEditorial;
            }

            var editorialesTask = await new ListarEditorialParameter().ExecuteAsync();
            var editorialBase = ((ListarEditorialResult)editorialesTask).Hits;
            saveCacheEditorial(editorialBase, keyMap);

            return editorialBase;
        }

        private void saveCacheEditorial(IEnumerable<EditorialDTO> result, string keyMap)
        {
            CatalagoManager.Instance.saveCache(result, keyMap);

        }

        private void saveCacheEspecialidad(IEnumerable<EspecialidadDTO> result, string keyMap)
        {
            CatalagoManager.Instance.saveCache(result, keyMap);
        }

        private void saveCacheBanner(IEnumerable<BannerDTO> result, string keyMap)
        {
            CatalagoManager.Instance.saveCache(result, keyMap);
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
