using log4net;

using ServiceAgents.Common;
using System;
using System.Reflection;
using System.Web.Mvc;
using Web.Common;
using Seguridad.Common;
using QueryContracts.Xmarket.Carrito.Result;
using Web.Xmarket.Models.Producto;
using QueryContracts.Xmarket.Book.Result;
using QueryContracts.Xmarket.Book.Parameters;
using QueryContracts.Xmarket.Carrito.Parameters;
using Utilitario.Common;
using QueryContracts.Xmarket.Book;
using static Utilitario.Common.ConstanteGeneral;
using System.Threading.Tasks;
using System.Web.UI;

namespace Web.Xmarket.Helpers.Controllers
{
    [AllowAnonymous]
    public class ProductoController : BaseController
    {
        private ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Index(string isbn)
        {

            ProductoModel model = new ProductoModel();

            try
            {
                model.bookResult = (BookLeerResult)new BookLeerParameter()
                {
                    isbn = isbn,

                }.Execute();

                if (model.bookResult.Hit != null)
                {
                    int? codigoCarrito = SessionWrapper.Instance.getCodigoCarritoCodigo();
                    LeerCarritoDetalleParameter leerCarritoDetalle = new LeerCarritoDetalleParameter();
                    leerCarritoDetalle.IdCarrito = Convert.ToInt32(codigoCarrito);
                    leerCarritoDetalle.IdProducto = model.bookResult.Hit.bookCodigo;
                    model.productoCarrito = (LeerCarritoDetalleResult)leerCarritoDetalle.Execute();


                }
                else
                {
                    model.bookResult.Hit = new BookDTO();
                }


            }
            catch (Exception err)
            {
                log.Error(err);
            }
            return View(model);
        }

  
        public async Task<ActionResult> productTitle(string title, string isbn)
        {
            ProductoModel model = new ProductoModel();

            try
            {
                model.bookResult = (BookLeerResult)await new BookLeerParameter()
                {
                    isbn = isbn,

                }.ExecuteAsync()
                ;

                if (model.bookResult.Hit != null)
                {
                    int? codigoCarrito = SessionWrapper.Instance.getCodigoCarritoCodigo();
                    LeerCarritoDetalleParameter leerCarritoDetalle = new LeerCarritoDetalleParameter();
                    leerCarritoDetalle.IdCarrito = Convert.ToInt32(codigoCarrito);
                    leerCarritoDetalle.IdProducto = model.bookResult.Hit.bookCodigo;
                    model.productoCarrito = (LeerCarritoDetalleResult)leerCarritoDetalle.Execute();

                    String url = Url.RouteUrl("BookDetail", new
                    {
                        title = model.bookResult.Hit.bookTitleUrl,
                        isbn = model.bookResult.Hit.bookISBN
                    });

                    BookDTO book = model.bookResult.Hit;

                    setMetadaHeader(url, book);
                }
                else
                {
                    model.bookResult.Hit = new BookDTO();
                }
            }
            catch (Exception err)
            {
                log.Error(err);
            }
            return View("Index", model);
        }

        public async Task<PartialViewResult> getProductRelation(string id)
        {
            string idEspecialidadRelacionado = id;
            var bookListarResult = (BookListarResult)await new BookListarParameter()
            {
                opcionFiltro = ConstanteGeneral.BOOK_FILTRO.FILTRO_POR_ESPECIALIDAD,
                idEspecialdiad = Convert.ToInt32(idEspecialidadRelacionado),
                registroInicio = 0,
                registroFin = ParametrosConfigurado.COMPAGINADO
            }.ExecuteAsync();

            var model = new SWeb.Xmarket.Utilitario.Utilitarios().obtenerProductSlide(bookListarResult.Hit, 6.00);

            return PartialView("_libro_relacionado", model);
        }

        private void setMetadaHeader(string urlLibro, BookDTO book)
        {
            string nombreLibro = book.bookTitulo.Trim();
            String imagenBook = book.bookImagen.Trim();

            ViewData[METADATA_WEB.TITULO] = $"Libro {nombreLibro} - Limedica - Literatura Medica EIRL";


            if (String.IsNullOrEmpty(book.bookMetdataTitulo))
            {
                ViewData[METADATA_WEB.OG_TITULO] = $"Libro {nombreLibro} - Limedica";
            }
            else
            {
                ViewData[METADATA_WEB.OG_TITULO] = book.bookMetdataTitulo + " - Limedica";
            }

            if (String.IsNullOrEmpty(book.bookMetdataDescripcion))
            {
                ViewData[METADATA_WEB.DESCRIPCION] = $"Libro {nombreLibro}, entrega " + "en Peru, Lima y a nivel nacional.";
                ViewData[METADATA_WEB.OG_DESCRIPCION] = $"Libro {nombreLibro}, entrega " + "en Peru, Lima y a nivel nacional.";
            }
            else
            {
                ViewData[METADATA_WEB.DESCRIPCION] = book.bookMetdataDescripcion;
                ViewData[METADATA_WEB.OG_DESCRIPCION] = book.bookMetdataDescripcion;
            }

            if (String.IsNullOrEmpty(book.bookMetdataSiteName))
            {
                ViewData[METADATA_WEB.OG_SITE_NAME] =$"Libro {nombreLibro} - Limedica - Literatura Medica EIRL";
            }
            else {
                ViewData[METADATA_WEB.OG_SITE_NAME] = book.bookMetdataSiteName;
            }

            ViewData[METADATA_WEB.OG_TYPE] = "website";
            
            ViewData[METADATA_WEB.OG_URL] = URL_WEB.LIMEDICA + urlLibro;
            ViewData[METADATA_WEB.OG_URL_IMAGE] = imagenBook;
            ViewData[METADATA_WEB.CANONICAL] = URL_WEB.LIMEDICA + urlLibro;

        }
    }
}
