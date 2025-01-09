
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
    public class AreaGeneralQuery : IQueryHandler<AreaGeneralParameter>
    {
        public QueryResult Handle(AreaGeneralParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("CO_EMPRESA", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.prm_codigo_empresa);
                parametros.Add("CO_AREA_PADRE", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.prm_codigo_padre);

                var Result = new AreaGeneralResult();
                Result.Hits = connection.Query<AreaGeneralDTO>(
                                    "dbo.usp_area_listar_general",
                                    parametros,
                                    commandType: CommandType.StoredProcedure );
                return Result;
            }

        }
    }
}