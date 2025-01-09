using log4net;

using ServiceAgents.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Web.Xmarket.Models.Home;
using Web.Common;
using Seguridad.Common;
using QueryContracts.Xmarket.Carrito.Result;
using Web.Xmarket.Models.Carrito;
using Web.Common.Comun;
using CommandContracts.Xmarket.Carrito;
using CommandContracts.Xmarket.Carrito.Output;
using QueryContracts.Xmarket.Carrito.Parameters;
using Utilitario.Common;
using QueryContracts.Xmarket.Book.Parameters;
using QueryContracts.Xmarket.Book.Result;
using QueryContracts.Xmarket.Carrito;
using QueryContracts.Xmarket.Parametro.Parameters;
using QueryContracts.Xmarket.Parametro.Result;
using QueryContracts.Xmarket.General.Parameters;
using QueryContracts.Xmarket.General.Result;
using Web.Xmarket.Entidad;
using QueryContracts.Xmarket.Book;
using Web.Common.HtmlHelpers;
using CommandContracts.Xmarket.Cuenta;
using Web.Xmarket.DataAccess.Seguridad;
using System.ServiceModel;
using QueryContracts.Xmarket.Cliente.Parameters;
using QueryContracts.Xmarket.Cliente.Result;
using QueryContracts.Xmarket.Cliente;
using WebGrease.Css.Extensions;
using Web.Xmarket.Utilitario;
using CommandContracts.Common;
using System.Text;
using CommandContracts.Xmarket.Pedido;
using Web.Xmarket.Models.Cliente;
using static Utilitario.Common.ConstanteGeneral;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Web.Xmarket.Helpers.Controllers
{
    [AllowAnonymous]
    public class CarritoController : BaseController
    {

        private ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

      

        public async Task<ActionResult> Index()
        {
            setMetadaHeader();

            CarritoModel model = new CarritoModel();
            int? codigoCarrito = SessionWrapper.Instance.getCodigoCarritoCodigo();
            var resultCArrito = (ListarCarritoResult)await new ListarCarritoParameter() { IdCarrito = codigoCarrito == null ? 0 : Convert.ToInt64(codigoCarrito) }.ExecuteAsync();
            model.carrito = resultCArrito.Hit;
            if (model.carrito != null)
            {
                if (model.carrito.carritoDetalle.Any())
                {
                    var bookListarResult = (BookListarResult)await new BookListarParameter()
                    {
                        opcionFiltro = ConstanteGeneral.BOOK_FILTRO.FILTRO_POR_ESPECIALIDAD,
                        idEspecialdiad = Convert.ToInt32(model.carrito.carritoDetalle.ElementAt(0).idEspecialidadRelacionado),
                        registroInicio = 0,
                        registroFin = ParametrosConfigurado.COMPAGINADO
                    }.ExecuteAsync();

                    model.productoRelacionSlides = obtenerProductSlide(bookListarResult.Hit);
                }
            }

            return View(model);
        }

        private List<ProductoSlide> obtenerProductSlide(IEnumerable<BookFiltroDTO> librosEbook)
        {


            return new SWeb.Xmarket.Utilitario.Utilitarios().obtenerProductSlide(librosEbook, 6.00);
        }

        public ActionResult CheckOut()
        {

            if (SessionHelper.Instance.GetUser() <= 0)
            {
                return RedirectToAction("checkoutentrega");
            }
            CheckOutModel model = new CheckOutModel();
            checkoutCommand(ref model);
            getCatalogoCheckOut(ref model);

            return View(model);

        }

        [HttpPost]
        public ActionResult CheckOut(CheckOutModel model)
        {
            if (SessionHelper.Instance.GetUser() <= 0)
            {
                return RedirectToAction("checkoutentrega");
            }

            int? codigoCarrito = SessionWrapper.Instance.getCodigoCarritoCodigo();

            if (ModelState.IsValid)
            {
                try
                {



                    model.registrarClienteContactoCommand.direcciones = new List<CommandContracts.Xmarket.Pedido.Direccion>();

                    var direccionSeleccionado = model.ClienteDireccionListarResult.direcciones.Where(x => x.seleccionado).FirstOrDefault();
                    if (direccionSeleccionado != null)
                    {
                        model.registrarClienteContactoCommand.direcciones.Add(new CommandContracts.Xmarket.Pedido.Direccion()
                        {
                            idDireccion = direccionSeleccionado.idDireccion,
                            tipoDireccion = direccionSeleccionado.tipoDireccion
                        });
                    }

                    SessionLocalManager.Instance.addSessionClienteDireccionCarrito(model.registrarClienteContactoCommand);

                    return RedirectToAction("checkoutpago");

                }
                catch (Exception err)
                {
                    log.Error("CheckOut Post", err);
                    ViewBag.MensajeError = "Ocurrio un eror inesperado, por favor volver a intentar.";
                    ViewBag.EstadoError = -1;
                    checkoutCommand(ref model);
                    getCatalogoCheckOut(ref model);
                }

            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                     .SelectMany(v => v.Errors)
                     .Select(e => e.ErrorMessage));
                ModelState.AddModelError("Error", message);

                ViewBag.MensajeError = message;
                ViewBag.EstadoError = 3;

                checkoutCommand(ref model);
                getCatalogoCheckOut(ref model);
            }

            return View(model);
        }

        private void checkoutCommand(ref CheckOutModel model)
        {
            int? codigoCarrito = SessionWrapper.Instance.getCodigoCarritoCodigo();

            var resultCArrito = (ListarCarritoResult)new ListarCarritoParameter()
            {
                IdCarrito = codigoCarrito == null ? 0 : Convert.ToInt64(codigoCarrito)
            }.Execute();

            model.carrito = resultCArrito.Hit;
            model.correo = this.Usuario.EmailUsuario;
            model.direccionCarrito = new DireccionCarritoModel();
            model.direccionCarrito.direcciones = new List<ClienteDireccionDTO>();

            if (model.registrarClienteContactoCommand == null)
            {
                model.registrarClienteContactoCommand = new CommandContracts.Xmarket.Pedido.RegistrarClienteContactoCommand();
                model.registrarClienteContactoCommand.contactoNumeroDocumento = "";
                model.registrarClienteContactoCommand.contactoTipoDocumento = "";
                model.registrarClienteContactoCommand.contactoTipoDocumentoPedido = "";
                model.registrarClienteContactoCommand.direcciones = new List<CommandContracts.Xmarket.Pedido.Direccion>();

            }

            ClienteDireccionDTO clienteDireccionDTO = new ClienteDireccionDTO();

            if (model.ClienteDireccionListarResult != null)
            {
                clienteDireccionDTO = model.ClienteDireccionListarResult.direcciones.Where(x => x.seleccionado).FirstOrDefault();
            }

            model.ClienteDireccionListarResult =
                (ClienteDireccionListarResult)new ClienteDireccionListarParameter()
                {
                    idUsuario = Convert.ToInt32(this.Usuario.Idusuario)
                }.Execute();

            if (clienteDireccionDTO != null)
            {
                var direccionSeleccionado = model.ClienteDireccionListarResult.direcciones.Where(x => x.idDireccion == clienteDireccionDTO.idDireccion).FirstOrDefault();
                if (direccionSeleccionado != null)
                    direccionSeleccionado.seleccionado = true;
            }

        }

        public ActionResult CheckOutCompletado()
        {

            CheckOutCompletadoModel model = new CheckOutCompletadoModel();


            return View(model);
        }

        public ActionResult CheckOutEntrega()
        {

            CheckOutEntregaModel model = new CheckOutEntregaModel();
            int? codigoCarrito = SessionWrapper.Instance.getCodigoCarritoCodigo();
            model.carrito = obtenerCarrito(codigoCarrito);
            getCatalogoCheckOurEntrega(ref model);
            return View(model);
        }

        [HttpPost]
        public ActionResult CheckOutEntrega(CheckOutEntregaModel request)
        {
            CheckOutEntregaModel model = request;
            int? codigoCarrito = SessionWrapper.Instance.getCodigoCarritoCodigo();

            if (ModelState.IsValid)
            {
                try
                {

                    string namewCookies = BaseCommon.Common.Comun.COOKIES_SESION_PUBLICO;
                    var cookiesPublico = CookiesManager.Instance.getCookie(this, namewCookies);
                    Session session = JsonConvert.DeserializeObject<Session>(cookiesPublico);

                    request.clienteSinSesionCommand.sesionPublica = session.CodSession.ToString();

                    var resultClientSinSesion = (RegistrarClienteSinSesionOutput)request.clienteSinSesionCommand.Execute();

                    if (resultClientSinSesion.Estado == 0)
                    {
                         SessionWrapper.Instance.setCookiesSesionPublico(resultClientSinSesion.idUsuarioLibre);
                        return RedirectToAction("CheckoutPago");
                    }
                    else
                    {
                        model.carrito = obtenerCarrito(codigoCarrito);
                        getCatalogoCheckOurEntrega(ref model);
                    }
                }
                catch (FaultException<CommandFault> err)
                {
                    log.Error(err);
                    model.carrito = obtenerCarrito(codigoCarrito);
                    getCatalogoCheckOurEntrega(ref model);
                }
                catch (Exception err)
                {
                    log.Error("CheckOut Post", err);
                    ViewBag.MensajeError = "Ocurrio un eror inesperado, por favor volver a intentar.";
                    ViewBag.EstadoError = -1;
                    model.carrito = obtenerCarrito(codigoCarrito);
                    getCatalogoCheckOurEntrega(ref model);
                }

            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                     .SelectMany(v => v.Errors)
                     .Select(e => e.ErrorMessage));
                ModelState.AddModelError("Error", message);

                ViewBag.MensajeError = message;
                ViewBag.EstadoError = 3;

                model.carrito = obtenerCarrito(codigoCarrito);
                getCatalogoCheckOurEntrega(ref model);

            }

            return View(model);
        }

        private void getCatalogoCheckOurEntrega(ref CheckOutEntregaModel model)
        {

            var parametros = GetOtherInfoAll();
            model.departamentos = parametros.departamentos;
            model.provincias = parametros.provincias;
            model.distritos = parametros.distritos;
            model.tipoDocumentosPedido = parametros.tipoDocumentosPedido;
            model.tipoDocumentos = parametros.tipoDocumentos;


        }

        private void getCatalogoCheckOut(ref CheckOutModel model)
        {

            var parametros = GetOtherInfoAll();

            model.tipoDocumentosPedido = parametros.tipoDocumentosPedido;
            model.tipoDocumentos = parametros.tipoDocumentos;

        }
        private CheckOutCarritoModel GetOtherInfoAll()
        {
            CheckOutCarritoModel model = new CheckOutCarritoModel();
            ListarParametrosParameter listarTipoDocumentoPedido = new ListarParametrosParameter();
            listarTipoDocumentoPedido.idParametroPadre = ParametroDatos.Parametro.TIPO_DOCUMENTO_PEDIDO;
            var listarTipoDocumentoPedidoResult = (ListarParametrosResult)listarTipoDocumentoPedido.Execute();

            ListarParametrosParameter listarTipoDocumento = new ListarParametrosParameter();
            listarTipoDocumento.idParametroPadre = ParametroDatos.Parametro.TIPO_DOCUMENTO_CONTACTO;
            var listarTipoDocumentoResult = (ListarParametrosResult)listarTipoDocumento.Execute();


            ListarDepartamentosParameter listarDepartamentoParameter = new ListarDepartamentosParameter();
            var listarDepartamentosResult = (ListarDepartamentosResult)listarDepartamentoParameter.Execute();


            ListarProvinciaParameter listarProvinciaParameter = new ListarProvinciaParameter();
            var listarProvinciaResult = (ListarProvinciaResult)listarProvinciaParameter.Execute();

            ListarDistritoParameter listarDistritoParameter = new ListarDistritoParameter();
            var listarDistritoResult = (ListarDistritoResult)listarDistritoParameter.Execute();


            model.departamentos = new SelectList(listarDepartamentosResult.Hits, "nombreDepartamento", "nombreDepartamento");
            model.provincias = new SelectList(listarProvinciaResult.Hits, "nombreProvincia", "nombreProvincia");
            model.distritos = new SelectList(listarDistritoResult.Hits, "nombreDistrito", "codigoDistrito");
            model.tipoDocumentosPedido = new SelectList(listarTipoDocumentoPedidoResult.Hits, "valorParametro", "nombreParametro");
            model.tipoDocumentos = new SelectList(listarTipoDocumentoResult.Hits, "valorParametro", "nombreParametro"); ;

            return model;
        }

        public JsonResult obtenerProvincias(ObtenerProvinciasModel request)
        {
            ResponseOperacionBE response = new ResponseOperacionBE();
            response.OperacionType = new OperacionType();
            response.OperacionType.codigo_operacion = "0";
            response.OperacionType.estado_operacion = "0";
            response.OperacionType.mensaje_operacion = "Registrado con éxito";

            ListarProvinciaParameter listarProvinciaParameter = new ListarProvinciaParameter();
            listarProvinciaParameter.departamento = request.nombreDepartamento;
            var listarProvinciaResult = (ListarProvinciaResult)listarProvinciaParameter.Execute();

            response.OperacionType.Objeto1 = listarProvinciaResult.Hits;

            return Json(response, JsonRequestBehavior.AllowGet);

        }
        public JsonResult obtenerDistrito(ObtenerDistritoModel request)
        {
            ResponseOperacionBE response = new ResponseOperacionBE();
            response.OperacionType = new OperacionType();
            response.OperacionType.codigo_operacion = "0";
            response.OperacionType.estado_operacion = "0";
            response.OperacionType.mensaje_operacion = "Registrado con éxito";

            ListarDistritoParameter listarDistritoParameter = new ListarDistritoParameter();
            listarDistritoParameter.departamento = request.nombreDepartamento;
            listarDistritoParameter.provincia = request.nombreProvincia;
            var listarDistritoResult = (ListarDistritoResult)listarDistritoParameter.Execute();

            response.OperacionType.Objeto1 = listarDistritoResult.Hits;

            return Json(response, JsonRequestBehavior.AllowGet);

        }


        private CarritoDTO obtenerCarrito(long? idcarrito)
        {

            var resultCArrito = (ListarCarritoResult)new ListarCarritoParameter() { IdCarrito = Convert.ToInt64(idcarrito) }.Execute();
            return resultCArrito.Hit;
        }

        private ListarParametrosResult getBancos()
        {
            ListarParametrosParameter listarTipoDocumentoPedido = new ListarParametrosParameter();
            listarTipoDocumentoPedido.idParametroPadre = ParametroDatos.Parametro.BANCO;
            var listarTipoDocumentoPedidoResult = (ListarParametrosResult)listarTipoDocumentoPedido.Execute();

            return listarTipoDocumentoPedidoResult;

        }
        private DireccionCarritoModel getDireccionCliente()
        {
            DireccionCarritoModel direccionCarrito = new DireccionCarritoModel();
            direccionCarrito.direcciones = new List<ClienteDireccionDTO>();
            return direccionCarrito;
        }

        public ActionResult CheckOutPago()
        {

            CheckOutPagoModel model = new CheckOutPagoModel();
            int? codigoCarrito = SessionWrapper.Instance.getCodigoCarritoCodigo();
            var resultCArrito = (ListarCarritoResult)new ListarCarritoParameter()
            {
                IdCarrito = codigoCarrito == null ? 0 : Convert.ToInt64(codigoCarrito)
            }.Execute();

            if (resultCArrito.Hit == null)
                return RedirectToAction("index", "Home"); ;

            if (SessionHelper.Instance.GetUser() > 0)
            {
                if (SessionLocalManager.Instance.getSessionClienteDireccionCarrito() == null)
                {
                    return RedirectToAction("checkout");
                }

                var direccionClienteSeleccionado = SessionLocalManager.Instance.getSessionClienteDireccionCarrito();

                var direccionesResult =
                ((ClienteDireccionListarResult)new ClienteDireccionListarParameter()
                {
                    idUsuario = Convert.ToInt32(this.Usuario.Idusuario)
                }.Execute());


                var direccionJoinResult = from direccion in direccionesResult.direcciones
                                          join direccionSeleccionado in direccionClienteSeleccionado.direcciones
                                          on direccion.idDireccion equals direccionSeleccionado.idDireccion
                                          select direccion;

                model.direcciones = direccionJoinResult.ToList();
            }

            model.carrito = resultCArrito.Hit;
            model.registrarPedidoCommand = new CommandContracts.Xmarket.Pedido.RegistrarPedidoCommand();
            model.banco = new SelectList(getBancos().Hits, "valorParametro", "nombreParametro");
            model.direccionCarrito = getDireccionCliente();

            return View(model);
        }

        [HttpPost]
        public ActionResult CheckOutPago(CheckOutPagoModel model)
        {
            int? codigoCarrito = SessionWrapper.Instance.getCodigoCarritoCodigo();
            try
            {
                if (ModelState.IsValid)
                {
                    

                    string namewCookies = BaseCommon.Common.Comun.COOKIES_SESION_PUBLICO;
                    var cookiesPublico = CookiesManager.Instance.getCookie(this, namewCookies);
                    Session session = JsonConvert.DeserializeObject<Session>(cookiesPublico);

                    model.registrarPedidoCommand.idCarrito = Convert.ToInt64(codigoCarrito);
                    model.registrarPedidoCommand.idUsuarioLibre = session.idUsuarioLibre;
                    model.registrarPedidoCommand.estado = ParametroDatos.Pedido.Estado.REGISTRADO;
                    model.registrarPedidoCommand.idSesion =session.CodSession;

                    if (SessionHelper.Instance.GetUser() > 0) // inicio de sesion
                    {
                        model.registrarPedidoCommand.idUsuarioCrea = SessionHelper.Instance.GetUser();

                        var direccionOtrosDatos = SessionLocalManager.Instance.getSessionClienteDireccionCarrito();

                        direccionOtrosDatos.direcciones.ForEach(direccion =>
                        {
                            DireccionCliente direccionCliente = new DireccionCliente();
                            direccionCliente.idDireccion = direccion.idDireccion;
                            direccionCliente.tipoDireccion = direccion.tipoDireccion;

                            model.registrarPedidoCommand.direccionClientes = new List<DireccionCliente>();
                            model.registrarPedidoCommand.direccionClientes.Add(direccionCliente);
                        });

                        model.registrarPedidoCommand.correo = this.Usuario.EmailUsuario;
                        model.registrarPedidoCommand.comprobantePago = direccionOtrosDatos.contactoTipoDocumentoPedido;
                        model.registrarPedidoCommand.tipoDocumento = direccionOtrosDatos.contactoTipoDocumento;
                        model.registrarPedidoCommand.numeroDocumento = direccionOtrosDatos.contactoNumeroDocumento;
                        model.registrarPedidoCommand.informacionFacturaEsIgualEntrega = direccionOtrosDatos.facturacionInfomoIgualEntrega;

                        model.registrarPedidoCommand.origenPedido = ParametroDatos.Pedido.TipoRegistroPedido.USUARIO_INICIO_SESION;
                    }
                    else
                    {
                        model.registrarPedidoCommand.origenPedido = ParametroDatos.Pedido.TipoRegistroPedido.USUARIO_NO_INICIO_SESION;
                    }

                    var result = (RegistrarPedidoOutput)model.registrarPedidoCommand.Execute();

                    if (result.Estado == 0)
                    {
                        ListarCarritoResult carritoResult =
                            (ListarCarritoResult)new ListarCarritoParameter() { IdCarrito = Convert.ToInt64(codigoCarrito) }.Execute();

                       
                        List<String> correos = new List<string>();

                        ParametrosConfigurado.CorreoDestinatario.Split(';').ToList().ForEach(correo =>
                        {
                            correos.Add(correo);
                        });

                        sendEmailClient(carritoResult, correos);

                        SessionLocalManager.Instance.deleteSessionClienteDireccionCarrito();
                        SessionWrapper.Instance.eliminarCarrito();
                        SessionWrapper.Instance.eliminarCantidadCarrito();

                        return RedirectToAction("checkoutcompletado", new { @idpedido = result.idPedido });
                    }
                    else
                    {
                        model.banco = new SelectList(getBancos().Hits, "valorParametro", "nombreParametro");
                        model.direccionCarrito = getDireccionCliente();
                        model.carrito = obtenerCarrito(codigoCarrito);
                    }

                }
                else
                {
                    var message = string.Join(" | ", ModelState.Values
                         .SelectMany(v => v.Errors)
                         .Select(e => e.ErrorMessage));
                    ModelState.AddModelError("Error", message);

                    ViewBag.MensajeError = message;
                    ViewBag.EstadoError = 3;

                    model.carrito = obtenerCarrito(codigoCarrito);
                    model.banco = new SelectList(getBancos().Hits, "valorParametro", "nombreParametro");
                    model.direccionCarrito = getDireccionCliente();
                }
            }
            catch (FaultException<CommandFault> err)
            {
                log.Error(err);

                StringBuilder v_valor = new StringBuilder();
                if (err.Detail.CommandErrors.Count() > 0)
                {
                    foreach (CommandError item in err.Detail.CommandErrors)
                    {
                        v_valor.Append(item.ErrorMessage);
                    }
                }
                else
                {
                    v_valor.Append(err.Message);
                }

                ViewBag.Mensaje = v_valor.ToString();
                ViewBag.Codigo = 2;

                model.carrito = obtenerCarrito(codigoCarrito);
                model.banco = new SelectList(getBancos().Hits, "valorParametro", "nombreParametro");
                model.direccionCarrito = getDireccionCliente();

            }
            catch (Exception ex)
            {
                log.Error(ex);
                ViewBag.Mensaje = "Error de sistema";
                ViewBag.Codigo = -1;

                model.carrito = obtenerCarrito(codigoCarrito);
                model.banco = new SelectList(getBancos().Hits, "valorParametro", "nombreParametro");
                model.direccionCarrito = getDireccionCliente();
            }

            return View(model);
        }

        public async Task sendEmailClient(ListarCarritoResult shoppingResult, List<string> emails)
        {

            await Task.Run(() =>
            {
                Diccionario diccionario = new Diccionario();


                diccionario.CorreosPara = emails;
                diccionario.Cuerpo = new FormatoCorreo().formatoEnviaPedidoCliente(shoppingResult);
                diccionario.Asunto = "Limedica - Pedido Registrado";
                diccionario.CorreosCopia.Add("jforesninaco@hotmail.com");
                Mailer mailer = new Mailer(diccionario);
                mailer.Enviar();
            });

          


        }

        public JsonResult AgregarProducto(AgregarCarritoModel request)
        {
            ResponseOperacionBE response = new ResponseOperacionBE();
            response.OperacionType = new OperacionType();
            response.OperacionType.codigo_operacion = "0";
            response.OperacionType.estado_operacion = "0";
            response.OperacionType.mensaje_operacion = "Registrado con éxito";

            int? codigoCarrito = SessionWrapper.Instance.getCodigoCarritoCodigo();

          

            var resultCArrito = (ListarCarritoResult)new ListarCarritoParameter() { IdCarrito = codigoCarrito == null ? 0 : Convert.ToInt64(codigoCarrito) }.Execute();

            var productoLeer = (BookLeerResult)new BookLeerParameter() { isbn = request.bookIsbn }.Execute();


            if (request.bookFormato.Equals(ConstanteGeneral.CARRITO.ITEM_FISICO) ||
                  request.bookFormato.Equals(ConstanteGeneral.CARRITO.ITEM_IBRIDO))
            {
                if (productoLeer.Hit.bookPrecio <= 0)
                {
                    response.OperacionType.estado_operacion = "1";
                    response.OperacionType.mensaje_operacion = "Estimado cliente, el precio del producto es igual o menos a cero, por favor comunicar a traves de contactenos.";
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                if (productoLeer.Hit.precioEBook <= 0)
                {
                    response.OperacionType.estado_operacion = "1";
                    response.OperacionType.mensaje_operacion = "Estimado cliente, el precio del producto es igual o menos a cero, por favor comunicar a traves de contactenos.";
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
            }

            if (resultCArrito.Hit == null)
            {

                string namewCookies = BaseCommon.Common.Comun.COOKIES_SESION_PUBLICO;
                var cookiesPublico = CookiesManager.Instance.getCookie(this, namewCookies);
                Session session = JsonConvert.DeserializeObject<Session>(cookiesPublico);

                var codigoSession = session.CodSession;

                CarritoRegistrarCommand carritoRegistrarCommand = new CarritoRegistrarCommand();
                carritoRegistrarCommand.Transaccion = CommandContracts.Common.Command.TipoTransaccion.Insertar;
                carritoRegistrarCommand.Carrito = new CommandContracts.Xmarket.Carrito.Carrito();
                carritoRegistrarCommand.Carrito.Cantidad = request.bookCantidadComprar;
                carritoRegistrarCommand.Carrito.CodigoEstado = 1;
                carritoRegistrarCommand.Carrito.FechaCarrito = DateTime.Now;
                carritoRegistrarCommand.Carrito.FechaCrea = DateTime.Now;
                carritoRegistrarCommand.Carrito.IdCodSesionPublico = SessionWrapper.Instance.getSesionPublico().CodSessionPulbico;
                carritoRegistrarCommand.Carrito.IdIpPublica = SessionWrapper.Instance.getSesionPublico().IpCliente;
                carritoRegistrarCommand.Carrito.Cantidad = request.bookCantidadComprar;

                carritoRegistrarCommand.Carrito.IdSesion = SessionWrapper.Instance.getSesionPublico().CodSession;
                carritoRegistrarCommand.Carrito.Latitud = null;
                carritoRegistrarCommand.Carrito.Longitud = null;
                carritoRegistrarCommand.Carrito.MontoDelivery = 0;
                carritoRegistrarCommand.Carrito.MontoIGV = 0;
                carritoRegistrarCommand.Carrito.IdSesion = codigoSession;

                if (request.bookFormato.Equals(ConstanteGeneral.CARRITO.ITEM_FISICO) ||
                    request.bookFormato.Equals(ConstanteGeneral.CARRITO.ITEM_IBRIDO))
                {
                    carritoRegistrarCommand.Carrito.MontoSubTotal = productoLeer.Hit.bookPrecio * request.bookCantidadComprar;
                    carritoRegistrarCommand.Carrito.TotalPagar = productoLeer.Hit.bookPrecio * request.bookCantidadComprar;
                }
                else
                {
                    carritoRegistrarCommand.Carrito.MontoSubTotal = productoLeer.Hit.precioEBook * request.bookCantidadComprar;
                    carritoRegistrarCommand.Carrito.TotalPagar = productoLeer.Hit.precioEBook * request.bookCantidadComprar;
                }

                List<CarritoDetalle> detalleCarrito = new List<CarritoDetalle>();
                CarritoDetalle producto = new CarritoDetalle();
                producto.IdProducto = request.bookId;
                producto.Cantidad = request.bookCantidadComprar;
                producto.TipoAccion = (int)ConstanteGeneral.TipoAccion.AgregarProducto;
                producto.Imagen = productoLeer.Hit.bookSoloNombreImagen;
                producto.formatoProducto = request.bookFormato;
                producto.NombreProducto = productoLeer.Hit.bookTitulo;
                producto.Precio = productoLeer.Hit.bookPrecio;
                producto.SubTotal = productoLeer.Hit.bookPrecio * request.bookCantidadComprar;
                producto.idSesion = codigoSession;

                if (SessionWrapper.Instance.Session != null)
                {
                    producto.idSesion = SessionWrapper.Instance.Session.CodSession;
                    producto.IdUsuarioCrear = Convert.ToInt32(SessionWrapper.Instance.Session.Usuario.Idusuario);
                }

                detalleCarrito.Add(producto);

                carritoRegistrarCommand.Carrito.Detalle = detalleCarrito;

                CarritoRegistrarOutput carritoRegistrarOutput = (CarritoRegistrarOutput)carritoRegistrarCommand.Execute();

                if (carritoRegistrarOutput.Estado == 0)
                {
                    response.OperacionType.Objeto1 = carritoRegistrarOutput;
                    // Fin leer carrito detalle
                    SessionManager.Instance.crearCookiesCarritoCodigo(Convert.ToInt32(carritoRegistrarOutput.IdCarrito));
                    // Agregar cantidad a cookies
                    SessionManager.Instance.addCantidadCarrito(
                        new SesionCarrito()
                        {
                            cantidadCarrito = carritoRegistrarOutput.CantidadProductos.ToString()
                        });
                    // Ini Leer carrito detalle

                    LeerCarritoDetalleParameter leerCarritoDetalle = new LeerCarritoDetalleParameter();
                    leerCarritoDetalle.IdCarrito = Convert.ToInt32(carritoRegistrarOutput.IdCarrito);
                    leerCarritoDetalle.IdProducto = request.bookId;
                    LeerCarritoDetalleResult leerCarritoDetalleResult = (LeerCarritoDetalleResult)leerCarritoDetalle.Execute();

                    if (leerCarritoDetalleResult.Hit != null)
                    {
                        CarritoDetalleRepsonse carritoDetalle = new CarritoDetalleRepsonse();
                        carritoDetalle.formatoProductoNombre = leerCarritoDetalleResult.Hit.formatoProductoNombre;
                        carritoDetalle.bookISBN = leerCarritoDetalleResult.Hit.bookISBN;
                        carritoDetalle.cantidadProducto = leerCarritoDetalleResult.Hit.cantidadProducto;
                        carritoDetalle.nombreProducto = leerCarritoDetalleResult.Hit.nombreProducto;
                        response.OperacionType.Objeto2 = carritoDetalle;
                    }
                }
                else
                {
                    response.OperacionType.estado_operacion = carritoRegistrarOutput.Estado.ToString();
                    response.OperacionType.mensaje_operacion = carritoRegistrarOutput.Mensaje;
                    response.OperacionType.Objeto3 = carritoRegistrarOutput.Observacion;
                }
            }
            else
            {
                CarritoAgregarCommand carritoAgregarCommand = new CarritoAgregarCommand();

                carritoAgregarCommand.Transaccion = CommandContracts.Common.Command.TipoTransaccion.Insertar;
                carritoAgregarCommand.Carrito = new CommandContracts.Xmarket.Carrito.Carrito();
                carritoAgregarCommand.Carrito.IdCarrito = Convert.ToInt32(codigoCarrito);
                carritoAgregarCommand.Carrito.CodigoEstado = 1;
                carritoAgregarCommand.Carrito.FechaCarrito = DateTime.Now;
                carritoAgregarCommand.Carrito.FechaCrea = DateTime.Now;
                carritoAgregarCommand.Carrito.IdCodSesionPublico = SessionWrapper.Instance.getSesionPublico().CodSessionPulbico;
                carritoAgregarCommand.Carrito.IdIpPublica = SessionWrapper.Instance.getSesionPublico().IpCliente;
                carritoAgregarCommand.Carrito.IdSesion = SessionWrapper.Instance.getSesionPublico().CodSession;

                List<CarritoDetalle> detalleCarrito = new List<CarritoDetalle>();
                CarritoDetalle producto = new CarritoDetalle();
                producto.IdProducto = request.bookId;
                producto.Cantidad = request.bookCantidadComprar;
                producto.TipoAccion = (int)ConstanteGeneral.TipoAccion.AgregarProducto;
                producto.Imagen = productoLeer.Hit.bookSoloNombreImagen;
                producto.formatoProducto = request.bookFormato;
                if (SessionWrapper.Instance.Session != null)
                {
                    producto.idSesion = SessionWrapper.Instance.Session.CodSession;
                    producto.IdUsuarioCrear = Convert.ToInt32(SessionWrapper.Instance.Session.Usuario.Idusuario);
                }

                producto.NombreProducto = productoLeer.Hit.bookTitulo;

                if (request.bookFormato.Equals(ConstanteGeneral.CARRITO.ITEM_FISICO) ||
                    request.bookFormato.Equals(ConstanteGeneral.CARRITO.ITEM_IBRIDO))
                {
                    producto.Precio = productoLeer.Hit.bookPrecio;
                    producto.SubTotal = productoLeer.Hit.bookPrecio * request.bookCantidadComprar;
                }
                else
                {
                    producto.Precio = productoLeer.Hit.precioEBook;
                    producto.SubTotal = productoLeer.Hit.precioEBook * request.bookCantidadComprar;
                }

                detalleCarrito.Add(producto);

                carritoAgregarCommand.Carrito.Detalle = detalleCarrito;
                CarritoAgregarOutput carritoAgregarOutput = (CarritoAgregarOutput)carritoAgregarCommand.Execute();

                if (carritoAgregarOutput.Estado == 0)
                {
                    response.OperacionType.Objeto1 = carritoAgregarOutput;
                    // Ini Leer carrito detalle
                    LeerCarritoDetalleParameter leerCarritoDetalle = new LeerCarritoDetalleParameter();
                    leerCarritoDetalle.IdCarrito = Convert.ToInt32(codigoCarrito);
                    leerCarritoDetalle.IdProducto = request.bookId;
                    LeerCarritoDetalleResult leerCarritoDetalleResult = (LeerCarritoDetalleResult)leerCarritoDetalle.Execute();

                    if (leerCarritoDetalleResult.Hit != null)
                    {
                        CarritoDetalleRepsonse carritoDetalle = new CarritoDetalleRepsonse();
                        carritoDetalle.formatoProductoNombre = leerCarritoDetalleResult.Hit.formatoProductoNombre;
                        carritoDetalle.bookISBN = leerCarritoDetalleResult.Hit.bookISBN;
                        carritoDetalle.cantidadProducto = leerCarritoDetalleResult.Hit.cantidadProducto;
                        carritoDetalle.nombreProducto = leerCarritoDetalleResult.Hit.nombreProducto;
                        response.OperacionType.Objeto2 = carritoDetalle;
                        // Agregar cantidad a cookies
                        SessionManager.Instance.addCantidadCarrito(
                            new SesionCarrito()
                            {
                                cantidadCarrito = carritoAgregarOutput.CantidadProductos.ToString()
                            });
                    }
                }
                else
                {
                    response.OperacionType.estado_operacion = carritoAgregarOutput.Estado.ToString();
                    response.OperacionType.mensaje_operacion = carritoAgregarOutput.Mensaje;
                    response.OperacionType.Objeto3 = carritoAgregarOutput.Observacion;
                }
            }

            return Json(response, JsonRequestBehavior.AllowGet);

        }

        public JsonResult eliminarProductoCarrito(EliminarProductoCarritoModel request)
        {
            ResponseOperacionBE response = new ResponseOperacionBE();
            response.OperacionType = new OperacionType();
            response.OperacionType.codigo_operacion = "0";
            response.OperacionType.estado_operacion = "0";
            response.OperacionType.mensaje_operacion = "Registrado con éxito";

            long? codigoCarrito = SessionWrapper.Instance.getCodigoCarritoCodigo();

            EliminarProductoCarritoCommand commandEliminarProductoCarrito = new EliminarProductoCarritoCommand()
            {
                idProducto = request.bookId,
                idCarrito = Convert.ToInt64(codigoCarrito)
            };

            if (SessionWrapper.Instance.Session != null)
            {
                commandEliminarProductoCarrito.idSesion = SessionWrapper.Instance.Session.CodSession;
                commandEliminarProductoCarrito.idUsuarioModifica = Convert.ToInt32(SessionWrapper.Instance.Session.Usuario.Idusuario);
            }

            var eliminarProductoCarritoOutPu = (EliminarProductoCarritoOutput)commandEliminarProductoCarrito.Execute();

            SessionManager.Instance.addCantidadCarrito(
                         new SesionCarrito()
                         {
                             cantidadCarrito = eliminarProductoCarritoOutPu.CantidadProductos.ToString()
                         });

            return Json(response, JsonRequestBehavior.AllowGet);

        }

        public JsonResult actualizarCarrito(ActualizarCarritoModel request)
        {
            ResponseOperacionBE response = new ResponseOperacionBE();
            response.OperacionType = new OperacionType();
            response.OperacionType.codigo_operacion = "0";
            response.OperacionType.estado_operacion = "0";
            response.OperacionType.mensaje_operacion = "Registrado con éxito";

            long? codigoCarrito = SessionWrapper.Instance.getCodigoCarritoCodigo();

            var resultMenoresIgualCero = request.productos.Where(x => x.bookCantidad <= 0);

            if (!resultMenoresIgualCero.Any())
            {

                var actualizarProductoCarrito = new ActualizarProductoCarritoCommand();
                List<CarritoDetalle> carritoDetalles = new List<CarritoDetalle>();

                foreach (var item in request.productos)
                {
                    CarritoDetalle carrDetalle = new CarritoDetalle();
                    carrDetalle.Cantidad = item.bookCantidad;
                    carrDetalle.IdProducto = item.bookId;
                    carritoDetalles.Add(carrDetalle);
                }

                if (SessionWrapper.Instance.Session != null)
                {
                    actualizarProductoCarrito.idSesion = SessionWrapper.Instance.Session.CodSession;
                    actualizarProductoCarrito.idUsuarioModifica = Convert.ToInt32(SessionWrapper.Instance.Session.Usuario.Idusuario);
                }

                actualizarProductoCarrito.Detalle = carritoDetalles;
                actualizarProductoCarrito.idCarrito = Convert.ToInt32(codigoCarrito);

                var actualizarProductoCarritoOutput = (ActualizarProductoCarritoOutput)actualizarProductoCarrito.Execute();

                SessionManager.Instance.addCantidadCarrito(
                             new SesionCarrito()
                             {
                                 cantidadCarrito = actualizarProductoCarritoOutput.CantidadProductos.ToString()
                             });
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult obtenerDireccion(string id)
        {
            UsuarioDireccionNuevoDTO model = new UsuarioDireccionNuevoDTO();
            ClienteDireccionLeerParameter clienteDireccion = new ClienteDireccionLeerParameter();
            clienteDireccion.idClienteDireccion = Convert.ToInt32(id);
            var result = (ClienteDireccionLeerResult)clienteDireccion.Execute();

            UsuarioDireccionNuevoDTO usuarioDireccionNuevoDTO = new UsuarioDireccionNuevoDTO();
            usuarioDireccionNuevoDTO.direccion = result.direccion;
            usuarioDireccionNuevoDTO.correlativo = 0;

            return PartialView("_direccion", usuarioDireccionNuevoDTO);
        }


        private void setMetadaHeader()
        {
            ViewData[METADATA_WEB.TITULO] = $"Carrito | Limedica 🚚✅";
            ViewData[METADATA_WEB.DESCRIPCION] = $"Carrito 🚚✅";

            ViewData[METADATA_WEB.OG_TITULO] = $"Carrito | Limedica 🚚✅";
            ViewData[METADATA_WEB.OG_DESCRIPCION] = $"Carrito 🚚✅";

            ViewData[METADATA_WEB.OG_TYPE] = "website";
            ViewData[METADATA_WEB.OG_SITE_NAME] = "Literatura Medica EIRL - libros profesionales para la salud";
            ViewData[METADATA_WEB.OG_URL] = URL_WEB.LIMEDICA + "/carrito";
            ViewData[METADATA_WEB.CANONICAL] = URL_WEB.LIMEDICA + "/carrito";
        }

    }
}