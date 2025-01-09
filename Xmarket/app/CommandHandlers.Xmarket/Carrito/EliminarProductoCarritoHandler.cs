
using CommandContracts.Common;
using CommandContracts.Xmarket.Carrito;
using CommandContracts.Xmarket.Carrito.Output;
using CommandHandlers.Common;
using Data.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CommandHandlers.Xmarket.Carrito
{
    public class EliminarProductoCarritoHandler : ICommandHandler<EliminarProductoCarritoCommand>
    {
        public EliminarProductoCarritoHandler()
        {
            
        }

        public CommandResult Handle(EliminarProductoCarritoCommand command)
        {          
            var OutPut = new EliminarProductoCarritoOutput();



            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("IdCarrito", dbType: DbType.Int64, direction: ParameterDirection.Input, value: command.idCarrito);
                parametros.Add("IdProducto", dbType: DbType.Int64, direction: ParameterDirection.Input, value: command.idProducto);
                parametros.Add("IdUsuarioModifica", dbType: DbType.Int32, direction: ParameterDirection.Input, value: command.idUsuarioModifica);
                parametros.Add("IdSesion", dbType: DbType.Int32, direction: ParameterDirection.Input, value: command.idSesion);
                parametros.Add("out_Cantidad", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("out_codigoresult", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("out_mensaje", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                connection.Query
                          (
                              "[ecommerce].[usp_carrito_proucto_eliminar]",
                              parametros,
                              commandType: CommandType.StoredProcedure
                          );

                OutPut.Mensaje = parametros.Get<string>("out_mensaje");
                OutPut.Estado = parametros.Get<Int32?>("out_codigoresult");
                OutPut.CantidadProductos = parametros.Get<Int32?>("out_Cantidad");
            }

            return OutPut;
        }
    }
}
