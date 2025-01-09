
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Xmarket.General.Parameters;
using QueryContracts.Xmarket.General.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.Xmarket.General
{
    public class ObtenerCorrelativoQuery : IQueryHandler<ObtenerCorrelativoParameter>
    {
        public QueryResult Handle(ObtenerCorrelativoParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("tipo", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.tipo);
                
               var result = connection.Query<ObtenerCorrelativoResult>(
                                    "dbo.usp_correlativo_generar",
                                    parametros,
                                    commandType: CommandType.StoredProcedure ).FirstOrDefault();
                return result;
            }

        }
    }
}
