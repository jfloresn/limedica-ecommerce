

using Data.Common;
using QueryContracts.Common;
using QueryContracts.Xmarket.Seguridad.Parameters;
using QueryContracts.Xmarket.Seguridad.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.Xmarket.Seguridad
{
    public class BuscarSistemasQuery : IQueryHandler<BuscarSistemasParameter>
    {
        public QueryResult Handle(BuscarSistemasParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("nombre", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.nombre);
                parametros.Add("alias", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.alias);
                parametros.Add("prm_reginicio", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.registro_inicio);
                parametros.Add("prm_regfin", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.registro_fin);
                parametros.Add("prm_regtotal", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var result = new BuscarSistemasResult();
                result.Hits = connection.Query<BuscarSistemasDto>(
                                    "dbo.pa_buscarsistemas_filtrar",
                                    parametros,
                                    commandType: CommandType.StoredProcedure).ToList();
                result.totalregistros = parametros.Get<System.Int32>("prm_regtotal");

                return result;
            }
        }
    }
}
