using System;

namespace Web.Common.ActionResults
{
    using System.Linq;
    using System.Reflection;
    using System.ServiceModel;
    using System.Web.Mvc;

    using CommandContracts.Common;

    using log4net;


    using StructureMap;

    public class CommandActionResult 
    {
        private Command command;
        private ActionResult successAction;
        private ViewResult errorView;
        private ActionResult DEFAULT_SUCCESS_ACTION = new RedirectResult("Index");
        private ViewResult DEFAULT_ERROR_VIEW = new ViewResult();
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public CommandActionResult()
        {
        }

        //Se puede modificar esta funcion para recibir tambien otro actionResult
        public CommandActionResult(Command command)
        {
            this.command = command;
            this.successAction = DEFAULT_SUCCESS_ACTION;
            this.errorView = DEFAULT_ERROR_VIEW;
        }

        public CommandActionResult(Command command, string urlResultRedirect)
        {
            DEFAULT_SUCCESS_ACTION = new RedirectResult(urlResultRedirect);
            this.command = command;
            this.successAction = DEFAULT_SUCCESS_ACTION;
            this.errorView = DEFAULT_ERROR_VIEW;

        }

        
        public CommandActionResult OnSuccess(ActionResult successAction)
        {
            this.successAction = successAction;
            return this;
        }

        public CommandActionResult OnError(ViewResult errorView)
        {
            this.errorView = errorView;
            return this;
        }

        private static void ObjetoMetadata(object objeto)
        {
            foreach (var infoMiembro in objeto.GetType().GetMembers().Where(infoMiembro => infoMiembro.MemberType == MemberTypes.Property))
            {
                log.Debug("::::::METADATA SERVER WEB::::::" + objeto.GetType().FullName + "  " + (infoMiembro).Name + " = " + ((PropertyInfo)infoMiembro).GetValue(objeto, null) + "-" + ((PropertyInfo)infoMiembro).PropertyType.FullName);
            }
        }
    }

    public enum ResultadoTransaccion
    {
        Exito = 1,
        Fallido = 2
    }

    public class Transaccion : CommandResult
    {
        public ActionResult ViewResult { get; set; }

        public object CommandResult { get; set; }

        public ResultadoTransaccion ResultadoTransaccion { get; set; }
    }
}
