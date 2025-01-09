
using CommandContracts.Common;
using CommandContracts.Xmarket.Cuenta;
using CommandContracts.Xmarket.General;
using CommandContracts.Xmarket.General.Output;
using CommandHandlers.Common;
using Data.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CommandHandlers.Xmarket.Seguridad
{
    public class ValidarUsuarioHandler : ICommandHandler<ValidarUsuarioCommand>
    {
        
        public ValidarUsuarioHandler()
        {
            
        }

        public CommandResult Handle(ValidarUsuarioCommand command)
        {

         
         
            var OutPut = new ValidarUsuarioOutput();

            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("usr_str_red", dbType: DbType.String, direction: ParameterDirection.Input, value: command.Usuario);
                parametros.Add("usr_str_password", dbType: DbType.String, direction: ParameterDirection.Input, value: command.Password);



                connection.Query
                    (
                        "seguridad.sp_validarusuario",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );



                return OutPut;
            }

        }
    }
}
