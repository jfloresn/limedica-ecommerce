using BaseCommon.Common;
using CommandContracts.Xmarket.Pedido;
using QueryContracts.Xmarket.Editorial;
using Seguridad.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utilitario.Common;

namespace Web.Xmarket.Utilitario
{
    public class SessionLocalManager
    {

     

        public SessionLocalManager()
        {
        }


        private static readonly Lazy<SessionLocalManager> _instance =
         new Lazy<SessionLocalManager>(() => new SessionLocalManager());

        public static SessionLocalManager Instance
        {
            get { return _instance.Value; }
        }


        public void addSessionClienteDireccionCarrito(RegistrarClienteContactoCommand registrarClienteContactoCommand) {
            HttpContext.Current.Session[ConstanteGeneral.WEB_SESSION_CLIENTE_DIRECCION_CARRITO] = registrarClienteContactoCommand;
        }

        public RegistrarClienteContactoCommand getSessionClienteDireccionCarrito()
        {
            return  (RegistrarClienteContactoCommand) HttpContext.Current.Session[ConstanteGeneral.WEB_SESSION_CLIENTE_DIRECCION_CARRITO];
        }


        public void deleteSessionClienteDireccionCarrito()
        {
            HttpContext.Current.Session.Remove(ConstanteGeneral.WEB_SESSION_CLIENTE_DIRECCION_CARRITO);
       
        }

    }
}