
using CommandContracts.Common;
using CommandContracts.Xmarket.Buscar;
using CommandContracts.Xmarket.Buscar.Output;
using CommandContracts.Xmarket.Carrito;
using CommandContracts.Xmarket.Carrito.Output;
using CommandContracts.Xmarket.Cliente;
using CommandContracts.Xmarket.Cliente.Output;
using CommandHandlers.Common;
using Data.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CommandHandlers.Xmarket.Buscar
{
    public class BuscarRegistrarHandler : ICommandHandler<BuscarRegistrarCommand>
    {
        public BuscarRegistrarHandler()
        {
            
        }

        public CommandResult Handle(BuscarRegistrarCommand command)
        {          
            var OutPut = new BuscarRegistrarOutput();
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@id_sesion", dbType: DbType.Int32, direction: ParameterDirection.Input, value: command.idSesion);
                parametros.Add("@idsesion_public", dbType: DbType.String, direction: ParameterDirection.Input, value: command.idSesionPublic);
                parametros.Add("@texto_busca", dbType: DbType.String, direction: ParameterDirection.Input, value: command.texto);
                parametros.Add("OUT_ESTADO", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("OUT_MENSAJE", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                connection.Query
                          (
                              "ecommerce.sp_registrar_busqueda",
                              parametros,
                              commandType: CommandType.StoredProcedure
                          );

                OutPut.Mensaje = parametros.Get<string>("OUT_MENSAJE"); 
                OutPut.Estado = parametros.Get<Int32?>("OUT_ESTADO");
               
            }

            return OutPut;
        }
    }
}
