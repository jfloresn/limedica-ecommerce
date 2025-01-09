
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Xmarket.Book;
using QueryContracts.Xmarket.Book.Parameters;
using QueryContracts.Xmarket.Book.Result;
using QueryContracts.Xmarket.Coleccion;
using QueryContracts.Xmarket.Coleccion.Parameters;
using QueryContracts.Xmarket.Coleccion.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.Xmarket.Carrito
{
    public class ColeccionListarQuery : IQueryHandler<ColeccionListarParameter>
    {
        public QueryResult Handle(ColeccionListarParameter parameters)
        {
            var result = new ColeccionListarResult();
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
               
                result.Hit = connection.Query<ColeccionDTO>(
                                    "[ecommerce].[usp_coleccion_listar]",
                                    null,
                                    commandType: CommandType.StoredProcedure).ToList();
            }

          
            return result;
        }
    }
}
