
using CommandContracts.Common;
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
    public class CuentaCrearModificarHandler : ICommandHandler<CuentaCrearModificarCommand>
    {
        
        public CuentaCrearModificarHandler()
        {
            
        }

        public CommandResult Handle(CuentaCrearModificarCommand command)
        {

            GeneraCodigo generaCodigo = new GeneraCodigo();
            string codigo = generaCodigo.GenerarCadenaLongit(6);
            var OutPut = new CuentaCrearModificarOutput();

            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                
                parametros.Add("Nombre", dbType: DbType.String, direction: ParameterDirection.Input, value: command.Nombre, size: 100);
                parametros.Add("ApellidoPaterno", dbType: DbType.String, direction: ParameterDirection.Input, value: command.ApellidoPaterno, size: 100);
                parametros.Add("ApellidoMaterno", dbType: DbType.String, direction: ParameterDirection.Input, value: command.ApellidoMaterno, size: 100);
                parametros.Add("Correo", dbType: DbType.String, direction: ParameterDirection.Input, value: command.Correo);
                parametros.Add("Clave", dbType: DbType.String, direction: ParameterDirection.Input, value: command.Password);
                parametros.Add("IdSesion", dbType: DbType.Int32, direction: ParameterDirection.Input, value: command.idSesion);
                parametros.Add("out_codigoCliente", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("out_codigoresult", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("out_mensaje", dbType: DbType.String, direction: ParameterDirection.Output, size:500);

                connection.Query
                    (
                        "ecommerce.cuenta_crea",
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                OutPut.Mensaje = parametros.Get<string>("out_mensaje");
                OutPut.Estado = parametros.Get<Int32>("out_codigoresult");
                OutPut.CodigoCliente = parametros.Get<Int32>("out_codigoCliente");

                return OutPut;
            }

        }
    }
}
