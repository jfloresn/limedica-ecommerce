
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
    public class BookLeerQuery : IQueryHandler<BookLeerParameter>
    {
        public QueryResult Handle(BookLeerParameter parameters)
        {
            var result = new BookLeerResult();
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                
                parametros.Add("isbn", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.isbn);
          

                result.Hit = connection.Query<BookDTO>(
                                    "[ecommerce].[usp_book_leer]",
                                    parametros,
                                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            return result;
        }
    }
}
