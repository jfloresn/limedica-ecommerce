
using CommandContracts.Common;

using CommandContracts.Xmarket.Sesion;
using CommandContracts.Xmarket.Sesion.Output;
using CommandHandlers.Common;
using Data.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CommandHandlers.Xmarket.Sesion
{
    public class CrearSesionHandler : ICommandHandler<CrearSesionCommand>
    {
        
        public CrearSesionHandler()
        {
            
        }

        public CommandResult Handle(CrearSesionCommand command)
        {

            var OutPut = new CrearSesionOutput();

            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("codSesionPublico", dbType: DbType.String, direction: ParameterDirection.Input, value: command.codigoSesionPublico, size:48);
                parametros.Add("IpPublica", dbType: DbType.String, direction: ParameterDirection.Input, value: command.IpPublica);

                parametros.Add("IdSesionWeb", dbType: DbType.String, direction: ParameterDirection.Input, value: command.idSessionWeb);
                parametros.Add("IdSesionVerificationToken", dbType: DbType.String, direction: ParameterDirection.Input, value: command.requestVerificationToken);

    

                parametros.Add("out_idSesion", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("out_codigoresult", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("out_mensaje", dbType: DbType.String, direction: ParameterDirection.Output, size:500);

                connection.Query
                    (
                        "[ecommerce].[usp_sesion_publica_crear]",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                OutPut.Mensaje = parametros.Get<string>("out_mensaje");
                OutPut.Estado = parametros.Get<Int32>("out_codigoresult");
                OutPut.CodigoSesion = parametros.Get<Int32>("out_idSesion");

                return OutPut;
            }

        }
    }
}
