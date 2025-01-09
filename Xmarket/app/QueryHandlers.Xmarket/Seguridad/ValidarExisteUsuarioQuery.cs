using Data.Common;
using QueryContracts.Xmarket.Seguridad.Parameters;
using QueryContracts.Xmarket.Seguridad.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.Xmarket.Seguridad
{
    public class ValidarExisteUsuarioQuery : IQueryHandler<ValidarExisteUsuarioParameter>
    {

        public QueryContracts.Common.QueryResult Handle(ValidarExisteUsuarioParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("usr_str_red", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.usr_str_red);
                parametros.Add("usr_str_email", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.usr_str_email);

                var resultado = new ValidarExisteUsuarioResult();
                resultado = connection.Query<ValidarExisteUsuarioResult>
                        (
                            "seguridad.pa_validarexisteusuario",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();
                return resultado;
            }
        }
    }
}
