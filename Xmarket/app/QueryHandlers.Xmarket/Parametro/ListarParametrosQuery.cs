
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Xmarket.General.Parameters;
using QueryContracts.Xmarket.General.Result;
using QueryContracts.Xmarket.Parametro;
using QueryContracts.Xmarket.Parametro.Parameters;
using QueryContracts.Xmarket.Parametro.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.Xmarket.Parametros
{
    public class ListarParametrosQuery : IQueryHandler<ListarParametrosParameter>
    {
        public QueryResult Handle(ListarParametrosParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();

                parametros.Add("IdParametroPadre", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idParametroPadre);

                var Result = new ListarParametrosResult();

                Result.Hits = connection.Query<ParametroDTO>(
                                    "ecommerce.sp_parametro_listar_padre",
                                    parametros,
                                    commandType: CommandType.StoredProcedure );
                return Result;
            }

        }
    }
}