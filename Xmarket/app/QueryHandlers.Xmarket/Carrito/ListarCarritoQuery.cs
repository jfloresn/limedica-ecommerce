
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
    public class ListarCarritoQuery : IQueryHandler<ListarCarritoParameter>
    {
        public QueryResult Handle(ListarCarritoParameter parameters)
        {
            var result = new ListarCarritoResult();
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("Idcarrito", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.IdCarrito);
                result.Hit = connection.Query<CarritoDTO>(
                                    "ecommerce.carrito_leer",
                                    parametros,
                                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            if (result.Hit !=null)
            {

                using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("Idcarrito", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.IdCarrito);

                    result.Hit.carritoDetalle = connection.Query<CarritoDetalleDTO>(
                                        "ecommerce.carrito_detalle_por_idcarrito",
                                        parametros,
                                        commandType: CommandType.StoredProcedure);
                }


                result.Hit.totalDetalleCarrito = result.Hit.carritoDetalle.Count();
            }

            return result;
        }
    }
}
