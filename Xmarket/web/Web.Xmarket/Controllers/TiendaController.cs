using QueryContracts.Xmarket.Book.Parameters;
using QueryContracts.Xmarket.Book.Result;
using ServiceAgents.Common;
using System;
using System.Web.Mvc;
using Web.Xmarket.Models.Tienda;
using Utilitario.Common;
using System.Reflection;
using log4net;
using QueryContracts.Xmarket.Especialidad.Result;
using QueryContracts.Xmarket.Especialidad.Parameters;
using QueryContracts.Xmarket.Editorial.Result;
using QueryContracts.Xmarket.Editorial.Parameters;

using static Utilitario.Common.ConstanteGeneral;
using static Utilitario.Common.ParametrosConfigurado;
using System.Threading.Tasks;

namespace Web.Xmarket.Controllers
{

    [AllowAnonymous]
    public class TiendaController : Controller
    {
        private ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public ActionResult Index(string typesearch,
                                    string edit,
                                    string codedit,
                                    string codespe,
                                    string espe,
                                    string search)
        {

            TiendaModel model = new TiendaModel(Url.Content("~/Content/assets/img/tienda-banner.jpg"));

            try
            {
                model.request = new ObtenerLibrosRequest();
                model.request.codEdit = codedit;
                model.request.codEspe = codespe;
                model.request.typeSearch = typesearch;
                model.request.espe = espe;
                model.request.edit = edit;
                model.request.criterioBusqueda = search;


                if (typesearch.Equals(BOOK_TYPE_SEARCH_URL.EDITORIAL))
                {

                    model.bookListarResult = (BookListarResult)new BookListarParameter()
                    {
                        opcionFiltro = BOOK_FILTRO.FILTRO_POR_EDITORIAL,
                        idEditorial = Convert.ToInt32(codedit),
                        registroInicio = 0,
                        registroFin = ParametrosConfigurado.COMPAGINADO
                    }.Execute();

                    model.editorial = (LeerEditorialResult)new LeerEditorialParameter() { idEditorial = Convert.ToInt32(model.request.codEdit) }.Execute();

                }
                else if (typesearch.Equals(BOOK_TYPE_SEARCH_URL.ESPECIALIDAD))
                {

                    model.bookListarResult = (BookListarResult)new BookListarParameter()
                    {
                        opcionFiltro = BOOK_FILTRO.FILTRO_POR_ESPECIALIDAD,
                        idEspecialdiad = Convert.ToInt32(model.request.codEspe),
                        registroInicio = 0,
                        registroFin = ParametrosConfigurado.COMPAGINADO
                    }.Execute();

                    model.especialidad = (LeerEspecialidadResult)new LeerEspecialidadParameter() { idEspecialidad = Convert.ToInt32(model.request.codEspe) }.Execute();


                }
                else if (typesearch.Equals(BOOK_TYPE_SEARCH_URL.E_BOOK))
                {
                    model.bookListarResult = (BookListarResult)new BookListarParameter()
                    {
                        opcionFiltro = BOOK_FILTRO.FILTRO_POR_BOOK,
                        registroInicio = 0,
                        registroFin = ParametrosConfigurado.COMPAGINADO
                    }.Execute();
                }
                else if (typesearch.Equals(BOOK_TYPE_SEARCH_URL.NOVEDADES))
                {
                    model.bookListarResult = (BookListarResult)new BookListarParameter()
                    {
                        opcionFiltro = BOOK_FILTRO.FILTRO_POR_NOVEDADES,
                        registroInicio = 0,
                        registroFin = ParametrosConfigurado.COMPAGINADO
                    }.Execute();
                }
                else if (typesearch.Equals(BOOK_TYPE_SEARCH_URL.BUSCAR))
                {
                    model.bookListarResult = (BookListarResult)new BookListarParameter()
                    {
                        opcionFiltro = BOOK_FILTRO.FILTRO_POR_BUSQUEDA,
                        criterioBusqueda = model.request.criterioBusqueda,
                        registroInicio = 0,
                        registroFin = ParametrosConfigurado.COMPAGINADO
                    }.Execute();
                }
            }
            catch (Exception err)
            {

                log.Error(err);
                model = new TiendaModel(Url.Content("~/Content/assets/img/tienda-banner.jpg"));

            }

            return View(model);
        }

        public async Task<ActionResult> ebook()
        {

            setMetadaHeaderEbook(Url.RouteUrl("TiendaEBook"));

            TiendaModel model = new TiendaModel(Url.Content("~/Content/assets/img/tienda-banner.jpg"));

            try
            {

                model.request = new ObtenerLibrosRequest();

                model.request.typeSearch = "eb";

            }
            catch (Exception err)
            {

                log.Error(err);
                model = new TiendaModel(Url.Content("~/Content/assets/img/tienda-banner.jpg"));

            }

            return View("index", model);
        }

        public async Task<ActionResult> editorial(string editorial, string id)
        {
            TiendaModel model = new TiendaModel(Url.Content("~/Content/assets/img/tienda-banner.jpg"));

            try
            {
                model.request = new ObtenerLibrosRequest();

                model.request.typeSearch = "ed";
                model.request.codEdit = id;

                model.editorial = (LeerEditorialResult)await new LeerEditorialParameter() { idEditorial = Convert.ToInt32(model.request.codEdit) }.ExecuteAsync();
                setMetadaHeaderPorEditorial(Url.RouteUrl("TiendaEditorialOpcion2", new { editorial = model.editorial.Hit.nombreUrl, id = id }),
                    model.editorial.Hit.nombre);

            }
            catch (Exception err)
            {

                log.Error(err);
                model = new TiendaModel(Url.Content("~/Content/assets/img/tienda-banner.jpg"));

            }

            return View("index", model);
        }

        public async Task<ActionResult> especialidad(string especialidad, string id)
        {
            TiendaModel model = new TiendaModel(Url.Content("~/Content/assets/img/tienda-banner.jpg"));

            try
            {
                model.request = new ObtenerLibrosRequest();
                model.request.typeSearch = "es";
                model.request.codEspe = id;

                model.especialidad = (LeerEspecialidadResult)await new LeerEspecialidadParameter() { idEspecialidad = Convert.ToInt32(model.request.codEspe) }.ExecuteAsync();

                model.imagen = model.especialidad.Hit.imagen;

                if (string.IsNullOrEmpty(model.especialidad.Hit.imagen))
                {
                    model.imagen = Url.Content("~/Content/assets/img/tienda-banner.jpg");
                }

                setMetadaHeaderPorEspecialidad(Url.RouteUrl("TiendaEspecialidadOpcion2", new { especialidad = model.especialidad.Hit.nombreUrl, id = id }),
                    model.especialidad.Hit.nombre);
            }
            catch (Exception err)
            {
                log.Error(err);
                model = new TiendaModel(Url.Content("~/Content/assets/img/tienda-banner.jpg"));

            }

            return View("index", model);
        }

        public async Task<ActionResult> novedades()
        {

            setMetadaHeaderNovedades(Url.RouteUrl("TiendaNovedades"));

            TiendaModel model = new TiendaModel(Url.Content("~/Content/assets/img/tienda-banner.jpg"));

            try
            {
                model.request = new ObtenerLibrosRequest();
                model.request.typeSearch = "nv";
            }
            catch (Exception err)
            {

                log.Error(err);
                model = new TiendaModel(Url.Content("~/Content/assets/img/tienda-banner.jpg"));

            }

            return View("index", model);
        }

        public async Task<ActionResult> buscar(string consulta)
        {
            TiendaModel model = new TiendaModel(Url.Content("~/Content/assets/img/tienda-banner.jpg"));

            try
            {
                model.request = new ObtenerLibrosRequest();
                model.request.typeSearch = "sa";
                model.request.criterioBusqueda = consulta;
            }
            catch (Exception err)
            {
                log.Error(err);
                model = new TiendaModel(Url.Content("~/Content/assets/img/tienda-banner.jpg"));

            }

            return View("index", model);
        }

        public async Task<PartialViewResult> obtenerLibrosCompaginados(ObtenerLibrosRequest obtenerLibrosRequest)
        {
            TiendaModel model = new TiendaModel(Url.Content("~/Content/assets/img/tienda-banner.jpg"));


            if (obtenerLibrosRequest.typeSearch.Equals(BOOK_TYPE_SEARCH_URL.EDITORIAL))
            {
                model.bookListarResult = (BookListarResult)await new BookListarParameter()
                {
                    opcionFiltro = BOOK_FILTRO.FILTRO_POR_EDITORIAL,
                    idEditorial = Convert.ToInt32(obtenerLibrosRequest.codEdit),
                    registroInicio = obtenerLibrosRequest.cantidadRegistroInicio + 1,
                    registroFin = (obtenerLibrosRequest.cantidadRegistroInicio + 1) + (obtenerLibrosRequest.cantidadRegistroCompaginado - 1)
                }.ExecuteAsync();
            }
            else if (obtenerLibrosRequest.typeSearch.Equals(BOOK_TYPE_SEARCH_URL.ESPECIALIDAD))
            {
                model.bookListarResult = (BookListarResult)await new BookListarParameter()
                {
                    opcionFiltro = BOOK_FILTRO.FILTRO_POR_ESPECIALIDAD,
                    idEspecialdiad = Convert.ToInt32(obtenerLibrosRequest.codEspe),
                    registroInicio = obtenerLibrosRequest.cantidadRegistroInicio + 1,
                    registroFin = (obtenerLibrosRequest.cantidadRegistroInicio + 1) + (obtenerLibrosRequest.cantidadRegistroCompaginado - 1)
                }.ExecuteAsync();
            }
            else if (obtenerLibrosRequest.typeSearch.Equals(BOOK_TYPE_SEARCH_URL.NOVEDADES))
            {
                model.bookListarResult = (BookListarResult)await new BookListarParameter()
                {
                    opcionFiltro = BOOK_FILTRO.FILTRO_POR_NOVEDADES,
                    registroInicio = obtenerLibrosRequest.cantidadRegistroInicio + 1,
                    registroFin = (obtenerLibrosRequest.cantidadRegistroInicio + 1) + (obtenerLibrosRequest.cantidadRegistroCompaginado - 1)
                }.ExecuteAsync();
            }
            else if (obtenerLibrosRequest.typeSearch.Equals(BOOK_TYPE_SEARCH_URL.E_BOOK))
            {
                model.bookListarResult = (BookListarResult)await new BookListarParameter()
                {
                    opcionFiltro = BOOK_FILTRO.FILTRO_POR_BOOK,
                    registroInicio = obtenerLibrosRequest.cantidadRegistroInicio + 1,
                    registroFin = (obtenerLibrosRequest.cantidadRegistroInicio + 1) + (obtenerLibrosRequest.cantidadRegistroCompaginado - 1)
                }.ExecuteAsync();
            }
            else if (obtenerLibrosRequest.typeSearch.Equals(BOOK_TYPE_SEARCH_URL.BUSCAR))
            {
                model.bookListarResult = (BookListarResult)await new BookListarParameter()
                {
                    opcionFiltro = BOOK_FILTRO.FILTRO_POR_BUSQUEDA,
                    criterioBusqueda = obtenerLibrosRequest.criterioBusqueda,
                    registroInicio = obtenerLibrosRequest.cantidadRegistroInicio + 1,
                    registroFin = (obtenerLibrosRequest.cantidadRegistroInicio + 1) + (obtenerLibrosRequest.cantidadRegistroCompaginado - 1)
                }.ExecuteAsync();

            }
            return PartialView("_libros_list", model.bookListarResult.Hit);
        }

        public async Task<PartialViewResult> obtenerLibrosAsync(string typeSearch, string idEditorial, string idEspecialidad, String criterio)
        {

            TiendaModel model = new TiendaModel(Url.Content("~/Content/assets/img/tienda-banner.jpg"));
            int registroInicio = 0;
            int registroFin = COMPAGINADO;


            if (typeSearch.Equals(BOOK_TYPE_SEARCH_URL.EDITORIAL))
            {
                model.bookListarResult = (BookListarResult)await new BookListarParameter()
                {
                    opcionFiltro = BOOK_FILTRO.FILTRO_POR_EDITORIAL,
                    idEditorial = Convert.ToInt32(idEditorial),
                    registroInicio = registroInicio,
                    registroFin = registroFin
                }.ExecuteAsync();
            }
            else if (typeSearch.Equals(BOOK_TYPE_SEARCH_URL.ESPECIALIDAD))
            {
                model.bookListarResult = (BookListarResult)await new BookListarParameter()
                {
                    opcionFiltro = BOOK_FILTRO.FILTRO_POR_ESPECIALIDAD,
                    idEspecialdiad = Convert.ToInt32(idEspecialidad),
                    registroInicio = registroInicio,
                    registroFin = registroFin
                }.ExecuteAsync();
            }
            else if (typeSearch.Equals(BOOK_TYPE_SEARCH_URL.NOVEDADES))
            {
                model.bookListarResult = (BookListarResult)await new BookListarParameter()
                {
                    opcionFiltro = BOOK_FILTRO.FILTRO_POR_NOVEDADES,
                    registroInicio = registroInicio,
                    registroFin = registroFin
                }.ExecuteAsync();
            }
            else if (typeSearch.Equals(BOOK_TYPE_SEARCH_URL.E_BOOK))
            {
                model.bookListarResult = (BookListarResult)await new BookListarParameter()
                {
                    opcionFiltro = BOOK_FILTRO.FILTRO_POR_BOOK,
                    registroInicio = registroInicio,
                    registroFin = registroFin
                }.ExecuteAsync();
            }
            else if (typeSearch.Equals(BOOK_TYPE_SEARCH_URL.BUSCAR))
            {
                model.bookListarResult = (BookListarResult)await new BookListarParameter()
                {
                    opcionFiltro = BOOK_FILTRO.FILTRO_POR_BUSQUEDA,
                    criterioBusqueda = criterio,
                    registroInicio = registroInicio,
                    registroFin = registroFin
                }.ExecuteAsync();

            }
            return PartialView("_libros_list_tienda", model.bookListarResult);
        }

        private void setMetadaHeaderNovedades(string url)
        {
            ViewData[METADATA_WEB.TITULO] = "Novedades en libros de medicina en lima Perú y a nivel nacional - Limedica - Literatura Medica EIRL 🚚✅";
            ViewData[METADATA_WEB.DESCRIPCION] = "Novedades en la venta de libros de medicina y veterinaria de las distintas editoriales y especialidades en Peru, Lima y a nivel nacional. 🚚✅";

            ViewData[METADATA_WEB.OG_TITULO] = "Novedades en libros de medicina en Perú, lima y a nivel nacional | Limedica";
            ViewData[METADATA_WEB.OG_DESCRIPCION] = "Novedades en la venta de libros de medicina y veterinaria de las distintas editoriales y especialidades en Peru, Lima y a nivel nacional. 🚚✅";

            ViewData[METADATA_WEB.OG_TYPE] = "website";
            ViewData[METADATA_WEB.OG_SITE_NAME] = URL_WEB.SITE_NAME;
            ViewData[METADATA_WEB.OG_URL] = URL_WEB.LIMEDICA + url;
            ViewData[METADATA_WEB.CANONICAL] = URL_WEB.LIMEDICA + url;
        }

        private void setMetadaHeaderEbook(string url)
        {
            ViewData[METADATA_WEB.TITULO] = "Libros e-book en lima Perú y a nivel nacional - Limedica - Literatura Medica EIRL 🚚✅";
            ViewData[METADATA_WEB.DESCRIPCION] = "Venta de libros e-book de medicina y veterinaria de las distintas editoriales y" +
                " especialidades en lima, Perú y a nivel nacional. - Limedica - Literatura Medica EIRL";

            ViewData[METADATA_WEB.OG_TITULO] = "Libros e-book de medicina en Perú, Lima y a nivel nacional | Limedica";
            ViewData[METADATA_WEB.OG_DESCRIPCION] = "Venta de libros e-book de medicina y veterinaria de las distintas editoriales y" +
                " especialidades en lima, Perú y a nivel nacional. - Limedica - Literatura Medica EIRL";
            ViewData[METADATA_WEB.OG_TYPE] = "website";
            ViewData[METADATA_WEB.OG_SITE_NAME] = URL_WEB.SITE_NAME;
            ViewData[METADATA_WEB.OG_URL] = URL_WEB.LIMEDICA + url;
            ViewData[METADATA_WEB.CANONICAL] = URL_WEB.LIMEDICA + url;
        }

        private void setMetadaHeaderPorEspecialidad(string url, string nombreEspecialidad)
        {
            ViewData[METADATA_WEB.TITULO] = $"Libro de {nombreEspecialidad} - Limedica - Literatura Medica EIRL 🚚✅";
            ViewData[METADATA_WEB.DESCRIPCION] = $"Libros de {nombreEspecialidad} de las distintas " +
                "editoriales en lima, Perú y a nivel nacional. - Limedica - Literatura Medica EIRL";

            ViewData[METADATA_WEB.OG_TITULO] = $"Libro de {nombreEspecialidad} - Limedica - Literatura Medica EIRL";
            ViewData[METADATA_WEB.OG_DESCRIPCION] = $"Libros de {nombreEspecialidad} de las distintas " +
                "editoriales en lima, Perú y a nivel nacional. - Limedica - Literatura Medica EIRL";

            ViewData[METADATA_WEB.OG_TYPE] = "website";
            ViewData[METADATA_WEB.OG_SITE_NAME] = URL_WEB.SITE_NAME;
            ViewData[METADATA_WEB.OG_URL] = URL_WEB.LIMEDICA + url;
            ViewData[METADATA_WEB.CANONICAL] = URL_WEB.LIMEDICA + url;
        }

        private void setMetadaHeaderPorEditorial(string urlEditorial, string nombreEditorial)
        {
            ViewData[METADATA_WEB.TITULO] = $"Libro de {nombreEditorial} - Limedica - Literatura Medica EIRL 🚚✅";
            ViewData[METADATA_WEB.DESCRIPCION] = $"Venta de libros de medicina de la editorial {nombreEditorial} de las distintas " +
                "especialidades en lima, Perú y a nivel nacional. - Limedica - Literatura Medica EIRL";

            ViewData[METADATA_WEB.OG_TITULO] = $"Libro de {nombreEditorial} - Limedica - Literatura Medica EIRL";
            ViewData[METADATA_WEB.OG_DESCRIPCION] = $"Libros de {nombreEditorial} de las distintas " +
                "especialidades en lima, Perú y a nivel nacional. - Limedica - Literatura Medica EIRL";

            ViewData[METADATA_WEB.OG_TYPE] = "website";
            ViewData[METADATA_WEB.OG_SITE_NAME] = URL_WEB.SITE_NAME;
            ViewData[METADATA_WEB.OG_URL] = URL_WEB.LIMEDICA + urlEditorial;
            ViewData[METADATA_WEB.CANONICAL] = URL_WEB.LIMEDICA + urlEditorial;
        }
    }
}
