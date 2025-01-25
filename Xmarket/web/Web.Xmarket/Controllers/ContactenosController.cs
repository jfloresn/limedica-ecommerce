using log4net;
using ServiceAgents.Common;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Web.Common;
using QueryContracts.Xmarket.General.Parameters;
using QueryContracts.Xmarket.General.Result;
using Web.Xmarket.Models.Contacto;
using Web.Common.Comun;
using CommandContracts.Xmarket.Contactenos.Output;
using Utilitario.Common;
using Web.Xmarket.Utilitario;
using System.Collections.Generic;
using CommandContracts.Xmarket.Contactenos;
using System.Threading;
using static Utilitario.Common.ConstanteGeneral;
using System.Threading.Tasks;
using CommandContracts.Common;
using StructureMap.Query;
using Seguridad.Common;
using System.Web.UI;

namespace Web.Xmarket.Helpers.Controllers
{
    [AllowAnonymous]
    public class ContactenosController : BaseController
    {

        private ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);



        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Server, NoStore = true)]

        public async  Task<ActionResult> Index()
        {
            setMetadaHeader();

            ContactoModel model = new ContactoModel();

            try
            {
                ListarDepartamentosParameter listarDepartamentoParameter = new ListarDepartamentosParameter();
                var listarDepartamentosResult = (ListarDepartamentosResult)await listarDepartamentoParameter.ExecuteAsync();
                model.departamentos = new SelectList(listarDepartamentosResult.Hits, "nombreDepartamento", "nombreDepartamento");
            }
            catch (Exception err)
            {
                log.Error(err);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(ContactoModel model)
        {
            setMetadaHeader();

            try
            {
                ListarDepartamentosParameter listarDepartamentoParameter = new ListarDepartamentosParameter();
                var listarDepartamentosResult = (ListarDepartamentosResult) await listarDepartamentoParameter.ExecuteAsync();
                model.departamentos = new SelectList(listarDepartamentosResult.Hits, "nombreDepartamento", "nombreDepartamento");
            }
            catch (Exception err)
            {
                log.Error(err);
            }

            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> registrarContacto(ContactoModel model)
        {
            ResponseOperacionBE response = new ResponseOperacionBE();
            response.OperacionType = new OperacionType();
            response.OperacionType.codigo_operacion = "0";
            response.OperacionType.estado_operacion = "0";
            response.OperacionType.mensaje_operacion = "Registrado con éxito";

            if (ModelState.IsValid)
            {
                var result = (ContactenosRegistrarOutput)await model.contactenosRegistrarCommand.ExecuteAsync();
                response.OperacionType.estado_operacion = result.Estado.ToString();
                response.OperacionType.mensaje_operacion = result.Mensaje;

                if (response.OperacionType.estado_operacion.Equals(ConstanteGeneral.RESPONSE_CORRECTO))
                {
                    enviarCorreo(model.contactenosRegistrarCommand, ParametrosConfigurado.CorreoDestinatario);
                }
            }
            else
            {
                response.errorForms = new List<ErrorForm>();
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


        public  void enviarCorreo(ContactenosRegistrarCommand command, string destinario)
        {
            Task.Run(async () =>
            {
                try
                {
                    await new GestionarCorreo().enviarCorreo(command, destinario);
                }
                catch (Exception ex)
                {
                    // Manejar la excepción según sea necesario
                    log.Error(ex);
                }
            });
        }


        public class GestionarCorreo
        {
            public async Task<int> enviarCorreo(ContactenosRegistrarCommand model, string correoAdministrador)
            {
                Diccionario diccionario = new Diccionario();
                List<String> correos = new List<string>();
                correos.Add(correoAdministrador);

                diccionario.CorreosPara = correos;
                diccionario.Cuerpo = new FormatoCorreo().plantillaContacto(model);
                diccionario.Asunto = "Limedica - Contacto";
                diccionario.CorreosCopia = new List<string>();

                diccionario.CorreosCopia.Add("jfloresninaco@gmail.com");
                Mailer mailer = new Mailer(diccionario);
                mailer.Enviar();
                return 0;
            }

        }

  


        public delegate void MatesCallback(int n);

        public void ResultCallback(int n)
        {
            Console.WriteLine("Resultado de la operación: " + n);
        }


        private void setMetadaHeader()
        {
            ViewData[METADATA_WEB.TITULO] = $"Contactenos | Limedica 🚚✅";
            ViewData[METADATA_WEB.DESCRIPCION] = $"Contactenos 🚚✅";

            ViewData[METADATA_WEB.OG_TITULO] = $"Contactenos | Limedica 🚚✅";
            ViewData[METADATA_WEB.OG_DESCRIPCION] = $"Contactenos 🚚✅";

            ViewData[METADATA_WEB.OG_TYPE] = "website";
            ViewData[METADATA_WEB.OG_SITE_NAME] = "Literatura Medica EIRL - libros profesionales para la salud";
            ViewData[METADATA_WEB.OG_URL] = URL_WEB.LIMEDICA + "/contactenos";
            ViewData[METADATA_WEB.CANONICAL] = URL_WEB.LIMEDICA + "/contactenos";
        }

    }
}