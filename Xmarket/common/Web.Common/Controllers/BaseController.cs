using System.Web.Mvc;
using CommandContracts.Common;
using Web.Common.ActionResults;
using Seguridad.Common;

using log4net;
using System.Reflection;
using System;
using System.Linq;
using System.Collections.Generic;
using QueryContracts.Common.Seguridad.Results;
using System.Configuration;
using QueryContracts.Common.Seguridad.Parameters;
using ServiceAgents.Common;

namespace Web.Common
{
    public class BaseController : Controller
    {
        private ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public SessionWrapper sessionWrapper = new SessionWrapper(new SessionManager());


        #region Metodos de Log

        public void Error(String error){
            Error(new Exception(error));
        }

        public void Error(Exception ex)
        {
            log.Error(ex.Message, ex);
            ex = this.Unwrap(ex);
            log.Error(ex.Message, ex);
        }

        private Exception Unwrap(Exception ex)
        {
            while (null != ex.InnerException)
            {
                ex = ex.InnerException;
            }

            return ex;
        }

        #endregion

        public CommandActionResult Command(Command command)
        {
            return new CommandActionResult(command);
        }

        public CommandActionResult Command(Command command, string urlResultRedirect)
        {
            return new CommandActionResult(command, urlResultRedirect);
        }

    

 

        private  List<MenuOpcion> ObtenerItemsMenu(string codigopadre, List<MenusxRolesDTO> listamenu)
        {
            var listamenures = new List<MenuOpcion>();
            foreach (var menu in listamenu.Where(x => x.pag_str_codmenu_padre == codigopadre).ToList())
            {
                MenuOpcion mnu = new MenuOpcion();
                mnu.NombreMenu = menu.pag_str_nombre;
                mnu.Url = menu.pag_str_url;
                mnu.IdMenuOpcion = menu.pag_int_id;
                mnu.Nivel = menu.pag_int_nivel;
                mnu.CodigoMenu = menu.pag_str_codmenu;
                mnu.TipoMenu = menu.pag_str_tipomenu;
                mnu.MenuItem = ObtenerItemsMenu(menu.pag_str_codmenu, listamenu);
                mnu.ItemSeleccionado = menu.srp_seleccion == 0 ? false : true;
                mnu.CodigoPermiso = menu.srp_str_codpermiso;
                mnu.ControllerName = menu.pag_str_controller;
                mnu.ActionName = menu.pag_str_action;
                mnu.AttributesRoute = menu.pag_str_attributes;
                mnu.Secuencia = menu.pag_int_secuencia;


                listamenures.Add(mnu);
            }
            return listamenures;
        }

        public Usuario Usuario
        {
            get
            {
             
                return sessionWrapper.Usuario; 
            }
        }

        public Session Session
        {
            get
            {
             
                return sessionWrapper.Session;
            }

        }

        public string GetModuleAcronym()
        {
            if (ConfigurationManager.AppSettings["ModuleAcronym"] == null) throw new ArgumentException("No esta configurado el ModuleAcronym en el archivo de configuración.");
            var res = Convert.ToString(ConfigurationManager.AppSettings["ModuleAcronym"]);
            return res;
        }
        
    }
}