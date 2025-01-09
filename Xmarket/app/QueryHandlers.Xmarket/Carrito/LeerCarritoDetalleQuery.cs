
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Xmarket.Carrito;
using QueryContracts.Xmarket.Carrito.Parameters;
using QueryContracts.Xmarket.Carrito.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.Xmarket.Carrito
{
    public class LeerCarritoDetalleQuery : IQueryHandler<LeerCarritoDetalleParameter>
    {
        public QueryResult Handle(LeerCarritoDetalleParameter parameters)
        {
            var result = new LeerCarritoDetalleResult();
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("IdCarrito", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.IdCarrito);
                parametros.Add("IdProducto", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.IdProducto);
                result.Hit = connection.Query<CarritoDetalleDTO>(
                                    "[ecommerce].[carrito_detalle_leer]",
                                    parametros,
                                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            

            return result;
        }
    }
}
