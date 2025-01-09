using Data.Common;
using QueryContracts.Common;
using QueryContracts.Xmarket.Account.Parameters;
using QueryContracts.Xmarket.Account.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


using QueryContracts.Xmarket.Seguridad.Parameters;
using QueryContracts.Xmarket.Seguridad.Result;

namespace QueryHandlers.Xmarket.Seguridad
{
    public class EliminarUsuarioQuery : IQueryHandler<EliminarUsuarioParameter>
    {

        public QueryContracts.Common.QueryResult Handle(EliminarUsuarioParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("usr_int_id", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.IdUsuario);

                var resultado = new EliminarUsuarioResult();
                resultado = connection.Query<EliminarUsuarioResult>
                        (
                            "seguridad.pa_eliminarusuario",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();
                return resultado;
            }
        }
    }
}
