using Seguridad.Common;
using System;
using System.Linq;
using System.DirectoryServices.AccountManagement;
using System.ServiceModel;
using System.Web.Mvc;
using System.Web.Security;
using Web.Common.AuthenticationServices;
using Web.Xmarket.Utilitario;
using Web.Xmarket.Models.Account;
using Web.Xmarket.Security;
using UserAuthentication;
using System.Configuration;
using Web.Xmarket.DataAccess.Seguridad;
using ServiceAgents.Common;
using log4net;
using System.Reflection;
using QueryContracts.Xmarket.General.Parameters;
using QueryContracts.Xmarket.General.Result;
using CommandContracts.Xmarket.General;
using CommandContracts.Xmarket.General.Output;
using Web.Xmarket.DataAccess.General;
using Web.Common.Comun;
using System.Text;
using CommandContracts.Common;

using ServiceAgents.Xmarket.BusService;
using BaseCommon.Common;
using Web.Common;

using Web.Common.HttpApplications.ActionFilters;
using Web.Xmarket.Helpers;
using Web.Common.HtmlHelpers;
using Web.Xmarket.Models.Cuenta;
using Utilitario.Common;
using Web.Xmarket.Models.Cliente;
using QueryContracts.Xmarket.Parametro.Parameters;
using QueryContracts.Xmarket.Parametro.Result;
using CommandContracts.Xmarket.Cliente.Output;
using Web.Xmarket.Entidad;
using static Utilitario.Common.ConstanteGeneral;
using System.Threading.Tasks;

namespace Web.Xmarket.Controllers
{
    public class CuentaController : BaseController
    {

        private ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

      

        [HttpGet]
        [AllowAnonymous]
        [OutputCache(Duration = 600, VaryByParam = "none")]
        public async Task<ActionResult> Index()
        {
            setMetadaHeader();

            var modelo = new SignInModel();

            modelo.ValidarUsuario = new CommandContracts.Xmarket.Cuenta.ValidarUsuarioCommand();

            var user=SessionHelper.Instance.GetUser();

            if (user > 0)
            {
                return RedirectToAction("micuenta");
            }

            return View(modelo);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> CrearCuenta()
        {
            CrearCuentaModel model = new CrearCuentaModel();
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> cuentacreada(string param)
        {
            CuentaCreadaModel model = new CuentaCreadaModel();
            string[] cadendas = SeguridadEncriptar.Instance.desemcriptar(param).Split('|');

            model.nombreCompleto = cadendas[0];

            return View(model);
        }
        
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> CrearCuenta(CrearCuentaModel model)
        {

            ViewBag.Mensaje = "Registrado con éxito";
            ViewBag.Codigo = 0;

            try
            {
                if (ModelState.IsValid)
                {
                    CuentaCrearModificarCommand crearCuentaCommand = new CuentaCrearModificarCommand();
                    crearCuentaCommand = model.crearCuenta;
                    crearCuentaCommand.Transaccion = CommandContracts.Common.Command.TipoTransaccion.Insertar;

                    CuentaCrearModificarOutput resultCrearCuenta = (CuentaCrearModificarOutput) await crearCuentaCommand.ExecuteAsync();

                    if (resultCrearCuenta.Estado == 0)
                    {
                        ViewBag.Mensaje = "Registrado con éxito";
                        ViewBag.Codigo = 0;


                        string param =$"{crearCuentaCommand.Nombre} {crearCuentaCommand.ApellidoMaterno}|{resultCrearCuenta.CodigoCliente.ToString()}";

                        return RedirectToAction("cuentacreada",new { 
                            param= SeguridadEncriptar.Instance.encriptar(param) });
                    }
                    else
                    {
                        ViewBag.Mensaje = resultCrearCuenta.Mensaje;
                        ViewBag.Codigo = resultCrearCuenta.Estado;
                    }
                }
                else
                {
                    ViewBag.Mensaje = "";
                    ViewBag.Codigo = 3;
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
            }
            catch (Exception ex)
            {
                log.Error(ex);


                ViewBag.Mensaje = "Error de sistema";
                ViewBag.Codigo = -1;
            }

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> MiCuenta()
        {
            setMetadaHeader();

            MiCuentaModel model = new MiCuentaModel();
            var usuario=SessionWrapper.Instance.Session.Usuario;
            model.nombreCompleto = $"{usuario.NombreUsuario} {usuario.ApellidoPaterno}" ;
            model.nombre = $"{usuario.NombreUsuario}";

            model.tipoCuenta =ConstanteGeneral.CUENTA_VISTA.MI_CUENTA;

            return View(model);
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> ordenes()
        {
            OrdenesModel model = new OrdenesModel();

            model.tipoCuenta =ConstanteGeneral.CUENTA_VISTA.ORDENES;
            return View(model);
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> DetalleCuenta()
        {
            DetalleCuentaModel model = new DetalleCuentaModel();
            model.tipoCuenta = ConstanteGeneral.CUENTA_VISTA.DETALLE_CUENTA;

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> MetodoPago()
        {
            MetodoPagoModel model = new MetodoPagoModel();


            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> Direcciones()
        {
            DireccionesModel model = new DireccionesModel();
            model.tipoCuenta = ConstanteGeneral.CUENTA_VISTA.DIRECCIONES;

            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> ValidarCuenta(string id)
        {

            var model = new ValidarCuentaModel();



            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<PartialViewResult> clienteDireccionRegistrar( )
        {
            ClienteDireccionModel model = new ClienteDireccionModel();
            ListarParametrosParameter listarTipoDireccion = new ListarParametrosParameter();
            listarTipoDireccion.idParametroPadre = ParametroDatos.Parametro.TIPO_DIRECCION;
            var listarTipoDirecionResult = (ListarParametrosResult) await listarTipoDireccion.ExecuteAsync();

            ListarDepartamentosParameter listarDepartamentoParameter = new ListarDepartamentosParameter();
            var listarDepartamentosResult = (ListarDepartamentosResult) await listarDepartamentoParameter.ExecuteAsync();


            ListarProvinciaParameter listarProvinciaParameter = new ListarProvinciaParameter();
            var listarProvinciaResult = (ListarProvinciaResult) await listarProvinciaParameter.ExecuteAsync();

            ListarDistritoParameter listarDistritoParameter = new ListarDistritoParameter();
            var listarDistritoResult = (ListarDistritoResult) await listarDistritoParameter.ExecuteAsync();


            model.departamentos = new SelectList(listarDepartamentosResult.Hits, "nombreDepartamento", "nombreDepartamento");
            model.provincias = new SelectList(listarProvinciaResult.Hits, "nombreProvincia", "nombreProvincia");
            model.distritos = new SelectList(listarDistritoResult.Hits, "nombreDistrito", "codigoDistrito");

            model.tipoDireccion = new SelectList(listarTipoDirecionResult.Hits, "valorParametro", "nombreParametro");

            model.direccionCommand = new CommandContracts.Xmarket.Cliente.RegistrarDireccionCommand();
            model.direccionCommand.ubigeoDepartamento = "";
            model.direccionCommand.ubigeoProvincia = "";
            model.direccionCommand.ubigeoDistrito = "";
            model.direccionCommand.tipoDireccion = ParametroDatos.ParametroValor.TIPO_DIRECCION.ENTREGA;

            return PartialView("_RegistrarDireccion",model);
        }

        [HttpPost]
        [AllowAnonymous]
        public  ActionResult Index(SignInModel modelo)
        {
         


            try
            {
                if (ModelState.IsValid)
                {
                    AccountData accountData = new AccountData();
                    var result =  accountData.ValidateUser(modelo.ValidarUsuario.Usuario, modelo.ValidarUsuario.Password);

              

                    if (result == "OK")
                    {
                        var resultado =    accountData.ObtenerUsuario(modelo.ValidarUsuario.Usuario);

              
                        if (resultado.Estatus.CodigoStatus == 0)
                        {
                            if (resultado.Hit != null)
                            {
                                SessionHelper.Instance.AddUserToSession(resultado.Hit.IdUsuario.ToString());

                                SessionManager sessionManager = new SessionManager();
                                sessionManager.crearSesionDeSesionPublica(resultado);

                                if(string.IsNullOrWhiteSpace(modelo.ReturnUrl))
                                    return RedirectToAction("micuenta");
                                else
                                    return RedirectToAction(modelo.ReturnUrl,"carrito");
                            }
                            else
                            {
                                ViewBag.Mensaje = "Correo o password incorrecto, vuelva intentar por favor";
                            }
                        }
                        else
                        {
                            ViewBag.Mensaje = resultado.Estatus.Mensaje;
                        }
                    }
                    else
                    {
                        ViewBag.Mensaje = result;
                    }
                }
            }
            catch (FaultException faulExcep)
            {
                log.Error(faulExcep);

                ViewBag.Mensaje = faulExcep.Message;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ViewBag.Mensaje = "Error de sistema, por favor volver a intentar.";
            }
            finally
            {
                modelo.ValidarUsuario.Password = "";
            }

            return View(modelo);

            

        }

        [AllowAnonymous]
        public ActionResult SignOut()
        {
            SessionManager.Instance.deleteCookies(BaseCommon.Common.Comun.COOKIES_SESION);
            SessionHelper.Instance.DestroyUserSession();

            return RedirectToAction("index");
        }

        [AllowAnonymous]
        public JsonResult GetLocalUser()
        {
            try
            {
                return Json(new { res = true, username = "" });
            }
            catch
            {
                return Json(new { res = false, msj = "No se pudo recuperar el usuario coorporativo"});
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> CambiarPassword()
        {
            CambiarPasswordModel o_CambiarPassword = new CambiarPasswordModel();
            //     SessionWrapper o_SessionWrapper = new SessionWrapper();
            //o_CambiarPassword.command = new CommandContracts.Xmarket.Seguridad.CambiarContrasenaCommand();
            //o_CambiarPassword.command.IdUsuario = o_SessionWrapper.Usuario.Idusuario;

            return View(o_CambiarPassword);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult registraClienteDireccion(ClienteDireccionModel model)
        {

            ResponseOperacionBE response = new ResponseOperacionBE();
            response.OperacionType = new OperacionType();
            response.OperacionType.codigo_operacion = "0";
            response.OperacionType.estado_operacion = "0";
            response.OperacionType.mensaje_operacion = "Registrado con éxito";

            UsuarioDireccionDTO usuarioDireccionDTO = new UsuarioDireccionDTO(); 

            if (ModelState.IsValid)
            {

                model.direccionCommand.idUsuarioCrea = this.Usuario.Idusuario;
                model.direccionCommand.idPais = ConstanteGeneral.PAIS.PERU;
                model.direccionCommand.idSesion = this.Session.CodSession;
                model.direccionCommand.idUsuario = this.Usuario.Idusuario;
                model.direccionCommand.estado = ConstanteGeneral.ESTADO.ACTIVO;

                var outRegistrarDireccion = (RegistrarDireccionOutput)model.direccionCommand.Execute();
                if (outRegistrarDireccion.Estado.Value ==0)
                {
                    usuarioDireccionDTO.idUsuarioDireccion = outRegistrarDireccion.IdUsuarioDireccion;
                    usuarioDireccionDTO.nombreContacto = model.direccionCommand.nombreContacto;
                    usuarioDireccionDTO.celularContacto = model.direccionCommand.celularContacto;
                    response.OperacionType.Objeto1 = usuarioDireccionDTO;
                }
                else if (outRegistrarDireccion.Estado.Value > 0)
                {
                    response.OperacionType.estado_operacion = "1";
                    response.OperacionType.mensaje_operacion = outRegistrarDireccion.Mensaje;

                }
                else if (outRegistrarDireccion.Estado.Value < 0)
                {
                    response.OperacionType.estado_operacion = "-1";
                    response.OperacionType.mensaje_operacion = "Ocurrio un error inesperado, por favor volver  intentar.";
                }
                
            }
            else
            {

                response.errorForms = new System.Collections.Generic.List<ErrorForm>();

                var erroresCampos = ModelState.Where(ms => ms.Value.Errors.Any())
                                        .Select(x => new { x.Key, x.Value.Errors });

                foreach (var erroneousField in erroresCampos)
                {
                    var fieldKey = erroneousField.Key;
                    var fieldErrors = string.Join(" | ", erroneousField.Errors.Select(e => e.ErrorMessage));

                    response.errorForms.Add(new ErrorForm()
                    {
                        mensaje = fieldErrors,
                        propiedad = fieldKey.Replace(".", "_")
                    });
                }


                response.OperacionType.estado_operacion = "1";


            }




            return Json(response, JsonRequestBehavior.AllowGet);

        }

        


        [AllowAnonymous]
        public ActionResult CambiarConfirmPassword()
        {
            CambiarConfirmPasswordModel o_CambiarConfirmPassword = new CambiarConfirmPasswordModel();
            o_CambiarConfirmPassword.Respuesta = "Se registro la contraseña nueva con éxito";

            return View(o_CambiarConfirmPassword);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult CambiarPassword(CambiarPasswordModel modelo)
        {
            CambiarPasswordModel o_CambiarPasswordModel = new CambiarPasswordModel();

            //if (modelo.command.Password.Trim() != modelo.PasswordNuevoRepetir.Trim())
            //{
            //    ViewBag.MensajeError = "La contraseña nueva no coencide";
            //    ViewBag.EstadoError = 1;
            //    return View(o_CambiarPasswordModel);
            //}

            if (ModelState.IsValid)
            {

                try
                {


                   // var o_CambiarPasswordResultado = (CambiarContrasenaOutput)modelo.command.Execute();
                   // ViewBag.MensajeError = o_CambiarPasswordResultado.CodigoRespuesta;
                   // ViewBag.EstadoError = o_CambiarPasswordResultado.Mensaje;

                    //if (o_CambiarPasswordResultado.CodigoRespuesta == 0)
                    //{
                    //    return RedirectToAction("CambiarConfirmPassword", "Account");
                    //}
                    //else
                    //{
                    //    return View(o_CambiarPasswordModel);
                    //}

                    return View(o_CambiarPasswordModel);

                }
                catch (FaultException err)
                {
                    ViewBag.MensajeError = err.Message;
                    ViewBag.EstadoError = 1;
                    return View(o_CambiarPasswordModel);

                }
                catch (Exception err)
                {
                    log.Error(err);
                    ViewBag.MensajeError = "Error de Sistema";
                    ViewBag.EstadoError = -2;
                    return View(o_CambiarPasswordModel);
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
                return View(o_CambiarPasswordModel);
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> ForgotPassword()
        {

            return View(new ForgotPasswordModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordModel modelo)
        {
            ForgotPasswordModel o_ForgotPasswordModelo = new ForgotPasswordModel();
            o_ForgotPasswordModelo = modelo;
        
            if (ModelState.IsValid)
            {
                try
                {
                    //UsuariosData usuariosData = new DataAccess.Seguridad.UsuariosData();

                    //var result = usuariosData.RecordarPassword(modelo.Email);
                    //if (result != null)
                    //{
                    //    return RedirectToAction("ConfirmPassword", "Account");
                    //}
                    //else
                    //{
                    //    ViewBag.MensajeError = "No existe el correo ingresado, por favor ingrese un correo válido";
                    //    ViewBag.EstadoError = 1;
                    //    return View(o_ForgotPasswordModelo);
                    //}
                    return View(o_ForgotPasswordModelo);
                }
                catch (FaultException err)
                {
                    log.Error(err);
                    ViewBag.MensajeError = err.Message;
                    ViewBag.EstadoError = 2;
                    return View(o_ForgotPasswordModelo);
                }
                catch (Exception err)
                {
                    log.Error(err);
                    ViewBag.MensajeError = "Error de Sistema";
                    ViewBag.EstadoError = -3;
                    return View(o_ForgotPasswordModelo);
                }
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                ModelState.AddModelError("Error", message);
                ViewBag.MensajeError = message;
                ViewBag.EstadoError = 4;
                return View(o_ForgotPasswordModelo);
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmPassword()
        {

            return View();
        }
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmPasswordError()
        {

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> SignInChange(string returnUrl = "",string id="0")
        {
          //  UsuariosData usuariosData = new DataAccess.Seguridad.UsuariosData();
            var algo = ViewBag.Usuario;
          //  var result = usuariosData.ResetarContraseña(this.ControllerContext, int.Parse(id));
            var modelo = new SignInModel();

            if (User.Identity.IsAuthenticated)
            {
                return SignOut();
            }
            modelo.ReturnUrl = returnUrl;
            return View(modelo);
        }


        private void setMetadaHeader()
        {
            ViewData[METADATA_WEB.TITULO] = "Mi cuenta| Limedica 🚚✅";
            ViewData[METADATA_WEB.DESCRIPCION] = "Mi Cuenta 🚚✅";

            ViewData[METADATA_WEB.OG_TITULO] = "Mi Cuenta | Limedica";
            ViewData[METADATA_WEB.OG_DESCRIPCION] = "Mi Cuenta";
            ViewData[METADATA_WEB.OG_TYPE] = "website";
            ViewData[METADATA_WEB.OG_SITE_NAME] = "Literatura Medica EIRL - libros profesionales para la salud";
            ViewData[METADATA_WEB.OG_URL] = URL_WEB.LIMEDICA + "/cuenta";
            ViewData[METADATA_WEB.CANONICAL] = URL_WEB.LIMEDICA + "/cuenta";
        }


    }
}
