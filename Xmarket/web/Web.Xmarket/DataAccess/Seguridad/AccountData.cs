
namespace Web.Xmarket.DataAccess.Seguridad
{
    using System.Text;

    using QueryContracts.Xmarket.Account.Parameters;
    using QueryContracts.Xmarket.Account.Results;
    using System;
    using System.Linq;
    using ServiceAgents.Common;
    using log4net;
    using System.Reflection;
    using QueryContracts.Xmarket.Cliente.Parameters;
    using QueryContracts.Xmarket.Cliente.Result;
    using System.Threading.Tasks;

    public  class AccountData
    {
        private ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);



        public  string ValidateUser(string username, string password)
        {
            var msjerr = new StringBuilder();

            var parametros = new ClienteLoginParameter { Correo = username, Clave = password };
            

            try
            {

                var resultado = (ClienteLoginResult)  parametros.Execute();
                if (resultado == null) throw new Exception("Error interno");

                if (resultado.Estatus.CodigoStatus != 0)
                {
                    msjerr.AppendLine(resultado.Estatus.Mensaje);
                 
                }
              
                if (String.IsNullOrEmpty(msjerr.ToString()) == false)
                    return msjerr.ToString();

                return "OK";
            }

            catch (Exception ex)

            {
                log.Error(ex);

                return "El usuario y/o la contraseña no existe";
            }
        }

        public  ClienteLeerCorreoResult ObtenerUsuario(string correo)
        {
            var parametro = new ClienteLeerCorreoParameter { Correo = correo };

            var resultado = (ClienteLeerCorreoResult)  parametro.Execute();


            return resultado;
        }

  

     

        
    }
}