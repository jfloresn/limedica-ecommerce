
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
    public class LeerEditorialQuery : IQueryHandler<LeerEditorialParameter>
    {
        public QueryResult Handle(LeerEditorialParameter parameters)
        {

            var result = new LeerEditorialResult();


            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();

                parametros.Add("idEditorial", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idEditorial);

                result.Hit = connection.Query<EditorialDTO>(
                                    "[ecommerce].[sp_editorial_leer]",
                                    parametros,
                                    commandType: CommandType.StoredProcedure ).FirstOrDefault();
                return result;
            }



        }
    }
}
