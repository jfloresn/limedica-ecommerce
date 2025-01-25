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
using System.Collections.Generic;

using System.IO;
using System.Linq;
using FuzzySharp;
using System.EnterpriseServices;
using QueryContracts.Xmarket.Book;
using Microsoft.Ajax.Utilities;
using QueryContracts.Xmarket.Banner.Parameters;
using QueryContracts.Xmarket.Banner.Results;
using QueryContracts.Xmarket.Banner;
using Web.Xmarket.DataAccess;
using Web.Xmarket.Utilitario;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using StructureMap.Query;
using Antlr.Runtime.Tree;
using Microsoft.Owin.Security.Provider;
using GroupDocs.Watermark.Contents.WordProcessing;
using Web.Common.Comun;
using System.Web.Http.Results;
using System.Web.Http;
using CommandContracts.Xmarket.Buscar;
using Seguridad.Common;
using Newtonsoft.Json;
using CommandContracts.Xmarket.Buscar.Output;

namespace Web.Xmarket.Controllers
{

    [System.Web.Mvc.AllowAnonymous]
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
            string imageBanner = Url.Content("~/Content/assets/img/tienda-banner.jpg");

            TiendaModel model = new TiendaModel(imageBanner);

            try
            {
            
;
                model.request = new ObtenerLibrosRequest();
                model.request.typeSearch = "sa";
                model.request.criterioBusqueda = consulta;
            }
            catch (Exception err)
            {
                log.Error(err);
                model = new TiendaModel(imageBanner);

            }

            return View("index", model);
        }

        public async Task<IEnumerable<string>> getIsbnContants(string userInput, int take, IEnumerable<string> options)
        {
            
           return options.Where(value => value.Contains(userInput)).Take(take);
           

        }
        
        public async Task<IEnumerable<string>> getSimilaritySuggestions(string userInput, int similarityPercent, int take, IEnumerable<string> options)
        {
            var suggestions = options.Where(option =>
            {
                int threshold = getSimilarityInput(option, similarityPercent);
                return Fuzz.Ratio(userInput, option) > threshold;
            })
                .OrderByDescending(option => Fuzz.Ratio(userInput, option))
               .Take(take);

            return suggestions;

        }

        private int getSimilarityInput(string option, int defaulSimilarity)
        {
            if (option.Length >= 40)
            {
                return 13;
            }

            return defaulSimilarity;
        }
        
        private async Task<IEnumerable<string>> readFile(string nameFile)
        {  
            List<string> strings = new List<string>();
            string filePath = this.ControllerContext.HttpContext.Server.MapPath("~/suggest/"+ nameFile);
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    strings.Add(line);
                }
            }

            return strings;
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

        public async Task<JsonResult> getSuggestion([FromBody] ObtenerLibrosRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.criterioBusqueda) || request.criterioBusqueda.Length < 3)
            {
                return Json(new List<CriterioBusqueda>(), JsonRequestBehavior.AllowGet);
            }

            try
            {
                string criterio = request.criterioBusqueda.Trim().ToLower();
                var list = await resultBookSuggest(criterio, registroInicio: request.cantidadRegistroInicio, registroFin: request.cantidadRegistroCompaginado)
                             .ConfigureAwait(false) ?? new List<CriterioBusqueda>();


                _ = Task.Run(() => registrarBusquedaAsync(criterio)).ConfigureAwait(false);


                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return Json(new { mensaje = "Error interno en el servidor." }, JsonRequestBehavior.AllowGet);
            }
        }



        private async Task registrarBusquedaAsync(string texto)
        {
            string namewCookies = BaseCommon.Common.Comun.COOKIES_SESION_PUBLICO;
            var cookiesPublico = CookiesManager.Instance.getCookie(this, namewCookies);

            if (string.IsNullOrWhiteSpace(cookiesPublico))
            {
                log.Warn("No se encontró la cookie de sesión pública.");
                return;
            }

            Session session;
            try
            {
                session = JsonConvert.DeserializeObject<Session>(cookiesPublico);
            }
            catch (JsonException ex)
            {
                log.Error("Error al deserializar la sesión pública.", ex);
                return;
            }

            if (session == null)
            {
                log.Warn("La sesión deserializada es nula.");
                return;
            }

            BuscarRegistrarCommand buscarRegistrarCommand = new BuscarRegistrarCommand
            {
                idSesion = session.CodSession,
                idSesionPublic = session.CodSessionPulbico,
                texto = texto
            };


            await GuardarBusquedaEnBD(buscarRegistrarCommand).ConfigureAwait(false);
        }

        private async Task GuardarBusquedaEnBD(BuscarRegistrarCommand command)
        {
            var result = (BuscarRegistrarOutput) await command.ExecuteAsync();

            log.Info($"Búsqueda registrada: {command.texto} - Sesión {command.idSesion}");
        }

        public async Task<PartialViewResult> obtenerLibrosAsync(string typeSearch, string idEditorial, string idEspecialidad, String criterio)
        {
            string partialView = "_libros_list_tienda";

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

                List<BookFiltroDTO> list = new List<BookFiltroDTO>();
                model.bookListarResult = new BookListarResult();
                model.bookListarResult.Hit = list;


                try
                {
                    if (String.IsNullOrEmpty(criterio))
                    {
                        return PartialView(partialView, model.bookListarResult);
                    }

                    criterio = criterio.Trim();

                    if (criterio.Length < 3)
                    {
                        return PartialView(partialView, model.bookListarResult);
                    }


                    criterio = criterio.ToLower();
                    model.bookListarResult = await resultBook(criterio, registroInicio, registroFin);
                    if (model.bookListarResult == null)
                    {
                        model.bookListarResult = new BookListarResult();
                    }

                    if (model.bookListarResult.Hit == null)
                    {
                        model.bookListarResult.Hit = new List<BookFiltroDTO>();
                    }

                }
                catch (Exception ex)
                {
                    log.Error(ex);


                }
            }

            return PartialView(partialView, model.bookListarResult);
        }

        private async Task<BookListarResult> resultBook(string criterio, int registroInicio, int registroFin)
        {
            string pattern = @"^[\d-]{4,}$";

            if (Regex.IsMatch(criterio, pattern))
            {
                return await resultBookForIsbnAsync(criterio, registroInicio, registroFin);
            }
            else
            {

                return await resultBookFilterAsync(criterio, registroInicio, registroFin);
            }
        }

        private async Task<IEnumerable<CriterioBusqueda>> resultBookSuggest(string criterio, int registroInicio, int registroFin)
        {
            string pattern = @"^[\d-]{4,}$";

            if (Regex.IsMatch(criterio, pattern))
            {
                return await resultBookSuggestForIsbnAsync(criterio, registroInicio, registroFin);
            }
            else
            {

                return await resultBookSuggestFilterAsync(criterio, registroInicio, registroFin);
            }
        }
        
        private async Task<BookListarResult> resultBookForIsbnAsync(string criterio, int registroInicio, int registroFin)
        {
            var startCacheTime = DateTime.UtcNow;
            log.Info($"Inicio de criterio book cache ISBN: {startCacheTime:O}");

            var listCriterioIsbn = await CriterioIsbn(criterio);

            log.Info($"Fin de criterio book cache ISBN: {DateTime.UtcNow:O}");


            if (listCriterioIsbn.Any())
            {
                log.Info($"Inicio de búsqueda de base de datos ISBN: {DateTime.UtcNow:O}");

                var bookSearchParameter = new BookSearchParameter
                {
                    registroInicio = registroInicio,
                    registroFin = registroFin,
                    criteriosBusqueda = listCriterioIsbn
                };

                var startSearchTime = DateTime.UtcNow;
                var bookListarResult = (BookListarResult)await bookSearchParameter.ExecuteAsync();

                log.Info($"Fin de búsqueda de base de datos ISBN: {DateTime.UtcNow:O}, Tiempo total: {(DateTime.UtcNow - startSearchTime).TotalMilliseconds}ms");
                return bookListarResult;
            }

            return new BookListarResult(); // Retorna un objeto vacío si no hay resultados
        }

        private async Task<IEnumerable<CriterioBusqueda>> resultBookSuggestForIsbnAsync(string criterio, int registroInicio, int registroFin)
        {
            var startCacheTime = DateTime.UtcNow;
            log.Info($"Inicio de criterio book cache ISBN: {startCacheTime:O}");

          
            var listCriterioIsbn = await CriterioIsbn(criterio);

            log.Info($"Fin de criterio book cache ISBN: {DateTime.UtcNow:O}");



            return listCriterioIsbn; // Retorna un objeto vacío si no hay resultados
        }

        private async Task<BookListarResult> resultBookFilterAsync(string criterio, int registroInicio, int registroFin)
        {
            var startCacheTime = DateTime.UtcNow;
            log.Info($"Inicio de criterio cache: {startCacheTime:O}");
            var startSearchTime = DateTime.UtcNow;

         

            var listCriteriosBook = await criterioBook(criterio);

            var resultCritreriSinDuplicidad = distinct(listCriteriosBook);

            log.Info($"Fin de criterio cache: {DateTime.UtcNow:O}, Tiempo total: {(DateTime.UtcNow - startSearchTime).TotalMilliseconds} ms");

            if (resultCritreriSinDuplicidad.Any())
            {
                log.Info($"Inicio de búsqueda de base de datos: {DateTime.UtcNow:O}");
                var startSearchTimeBd = DateTime.UtcNow;

                var bookSearchParameter = new BookSearchParameter
                {
                    registroInicio = registroInicio,
                    registroFin = registroFin,
                    criteriosBusqueda = resultCritreriSinDuplicidad
                };


                var bookListarResult = (BookListarResult)await bookSearchParameter.ExecuteAsync();

                log.Info($"Fin de búsqueda de base de datos: {DateTime.UtcNow:O}, Tiempo total: {(DateTime.UtcNow - startSearchTimeBd).TotalMilliseconds}ms");
                return bookListarResult;
            }

            return new BookListarResult(); // Retorna un objeto vacío si no hay resultados
        }

        private async Task<IEnumerable<CriterioBusqueda>> resultBookSuggestFilterAsync(string criterio, int registroInicio, int registroFin)
        {
            var startCacheTime = DateTime.UtcNow;
            log.Info($"Inicio de criterio cache: {startCacheTime:O}");
            var startSearchTime = DateTime.UtcNow;


            var listCriteriosBook = await criterioBook(criterio);

            var resultCritreriSinDuplicidad = distinct(listCriteriosBook);

            log.Info($"Fin de criterio cache: {DateTime.UtcNow:O}, Tiempo total: {(DateTime.UtcNow - startSearchTime).TotalMilliseconds} ms");



            return resultCritreriSinDuplicidad; // Retorna un objeto vacío si no hay resultados
        }
        
        private IEnumerable<CriterioBusqueda> distinct(IEnumerable<CriterioBusqueda> riterios)
        {
            var seenIds = new HashSet<long>();
            var listaSinDuplicados = new List<CriterioBusqueda>();

            foreach (var criterio in riterios)
            {
                if (seenIds.Add(criterio.id)) // Agrega solo si el Id no existe
                {
                    listaSinDuplicados.Add(criterio);
                }
            }

            return listaSinDuplicados;
        }

        private async Task<List<CriterioBusqueda>> criterioBook(string criterio)
        {
            var criterioBase = await readBook(); // Espera el resultado
            var resultTitulo = await getSimilaritySuggestions(criterio, 30, 40, criterioBase); // Luego ejecuta la siguiente tarea

            // Configuración de prefijos, offsets y límites
            var mappings = new Dictionary<string, (string Prefix, int Offset, int Limit)>
            {
                { "AU", ("a[", 2, 5) },
                { "ES", ("e[", 2, 2) },
                { "CO", ("c[", 2, 1) },
                { "TI", ("l[", 2, 15) }
            };

            // Procesamiento funcional
            var criteriosBusquedas = resultTitulo
                .SelectMany(x => mappings
                    .Select(mapping =>
                    {
                        var (type, (prefix, offset, limit)) = (mapping.Key, mapping.Value);
                        var position = x.LastIndexOf(prefix, StringComparison.Ordinal);

                        if (position > 0)
                        {
                            var value = x.Substring(position + offset, x.Length - position - offset - 1);
                            return new
                            {
                                Type = type,
                                Id = long.Parse(value),
                                Criterio = x,
                                Position = position
                            };
                        }

                        return null;
                    })
                    .Where(result => result != null)
                )
                .GroupBy(r => r.Type) // Agrupar por tipo para aplicar el límite
                .SelectMany(group => group
                    .Take(mappings[group.Key].Limit) // Tomar solo los elementos permitidos
                    .Select(r => new CriterioBusqueda
                    {
                        id = r.Id,
                        type = r.Type,
                        criterio = r.Criterio
                    })
                )
                .ToList();

            return criteriosBusquedas;
        }

        private async Task<List<CriterioBusqueda>> CriterioIsbn(string criterio)
        {
            var creteriosOtros = await readIsbn();
            var resultOtros = await getIsbnContants(criterio, 40, creteriosOtros);

            // Usamos LINQ para transformar el resultado en una lista de CriterioBusqueda
            return resultOtros.Select(value => new CriterioBusqueda
            {
                id = 0,
                type = "IS",
                criterio = value
            }).ToList();
        }

        private async Task<IEnumerable<string>> readAutor()
        {   
            string keyMap = "cache_file_autor";

            var cacheBanner = (IEnumerable<string>)CatalagoManager.Instance.getCache(keyMap);

            if (cacheBanner != null && cacheBanner.Any())
            {
                return cacheBanner;
            }

            var creteriosAutor = await readFile("autor.txt");



            CatalagoManager.Instance.saveCache(creteriosAutor, keyMap);

        

            return creteriosAutor;

        }

        private async Task<IEnumerable<string>> readBook()
        {
            string keyMap = "cache_file_book";

            var cacheBook = (IEnumerable<string>)CatalagoManager.Instance.getCache(keyMap);

            if (cacheBook != null && cacheBook.Any())
            {
                return cacheBook;
            }

            var readBok = await readFile("libro-titulo.txt");



            CatalagoManager.Instance.saveCache(readBok, keyMap);



            return readBok;

        }

        private async Task<IEnumerable<string>> readIsbn()
        {
            string keyMap = "cache_file_isbn";

            var cacheOtros = (IEnumerable<string>)CatalagoManager.Instance.getCache(keyMap);

            if (cacheOtros != null && cacheOtros.Any())
            {
                return cacheOtros;
            }

            var readOtros = await readFile("isbn.txt");

            CatalagoManager.Instance.saveCache(readOtros, keyMap);

            return readOtros;
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
