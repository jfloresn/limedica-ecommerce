
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Xmarket.Book;
using QueryContracts.Xmarket.Book.Parameters;
using QueryContracts.Xmarket.Book.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.Xmarket.Carrito
{
    public class BookFiltroQuery : IQueryHandler<BookFiltroParameter>
    {
        public QueryResult Handle(BookFiltroParameter parameters)
        {
            var result = new BookFiltroResult();
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                
                parametros.Add("opcion", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.opcionFiltro);
                parametros.Add("idEditorial", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idEditorial);
                parametros.Add("idEspecialidad", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idEspecialdiad);
              
                result.Hit = connection.Query<BookFiltroDTO>(
                                    "[ecommerce].[usp_book_filtro]",
                                    parametros,
                                    commandType: CommandType.StoredProcedure).ToList();
            }

            

            return result;
        }
    }
}
