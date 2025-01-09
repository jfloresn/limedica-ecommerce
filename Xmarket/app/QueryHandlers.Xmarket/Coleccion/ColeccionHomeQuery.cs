
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
    public class ColeccionHomeQuery : IQueryHandler<ColeccionHomeParameter>
    {
        public QueryResult Handle(ColeccionHomeParameter parameters)
        {
            var result = new ColeccionHomeResult();
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
               
                result.Hit = connection.Query<ColeccionDTO>(
                                    "[ecommerce].[usp_coleccion_home]",
                                    null,
                                    commandType: CommandType.StoredProcedure).ToList();
            }

          
            return result;
        }
    }
}
