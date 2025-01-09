
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
    public class LeerColeccionQuery : IQueryHandler<LeerColeccionParameter>
    {
        public QueryResult Handle(LeerColeccionParameter parameters)
        {
            var result = new LeerColeccionResult();
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idColeccion", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idColeccion);

                result.Hit = connection.Query<ColeccionDTO>(
                                    "[ecommerce].[usp_coleccion_leer]",
                                    parametros,
                                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

          
            return result;
        }
    }
}
