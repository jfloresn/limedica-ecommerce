
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
    public class ObtenerPasswordPorUsuarioQuery : IQueryHandler<ObtenerPasswordPorUsuarioParameter>
    {
        public QueryContracts.Common.QueryResult Handle(ObtenerPasswordPorUsuarioParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("CO_USUARIO", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.CodUsuario);

                var resultado = new ObtenerPasswordPorUsuarioResult();
                resultado = connection.Query<ObtenerPasswordPorUsuarioResult>
                        (
                            "seguridad.usp_sel_usuario_passporcodigo",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();
                return resultado;
            }
        }
    }
}
