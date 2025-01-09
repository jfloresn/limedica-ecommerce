
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
    public class ListarEditorialQuery : IQueryHandler<ListarEditorialParameter>
    {
        public QueryResult Handle(ListarEditorialParameter parameters)
        {

            var result = new ListarEditorialResult();


            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                result.Hits = connection.Query<EditorialDTO>(
                                    "[ecommerce].[sp_editorial_listar]",
                                    null,
                                    commandType: CommandType.StoredProcedure );
                return result;
            }



        }
    }
}
