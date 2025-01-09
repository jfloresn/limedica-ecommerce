
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Xmarket.Editorial;
using QueryContracts.Xmarket.Editorial.Parameters;
using QueryContracts.Xmarket.Editorial.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.Xmarket.Editorial
{
    public class ListarTodoEditorialQuery : IQueryHandler<ListarTodoEditorialParameter>
    {
        public QueryResult Handle(ListarTodoEditorialParameter parameters)
        {

            var result = new ListarTodoEditorialResult();


            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                result.Hits = connection.Query<EditorialDTO>(
                                    "[ecommerce].[sp_editorial_listar_todo]",
                                    null,
                                    commandType: CommandType.StoredProcedure );
                return result;
            }



        }
    }
}
