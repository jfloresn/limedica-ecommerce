
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
    public class ObtenerSistemaQuery : IQueryHandler<ObtenerSistemaParameter>
    {
        public QueryResult Handle(ObtenerSistemaParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("sis_int_id", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.sis_int_id);

                var resultado = new ObtenerSistemaResult();
                resultado = connection.Query<ObtenerSistemaResult>
                        (
                            "seguridad.pa_obtenersistema",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();
                return resultado;
            }
        }
    }
}
