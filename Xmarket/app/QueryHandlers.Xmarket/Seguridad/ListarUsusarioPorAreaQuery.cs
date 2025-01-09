using Data.Common;
using QueryContracts.Common;
using QueryContracts.Xmarket.Seguridad.Parameters;
using QueryContracts.Xmarket.Seguridad.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;

namespace QueryHandlers.Xmarket.Seguridad
{
    public class ListarUsuarioPorAreaQuery : IQueryHandler<ListarUsuarioAreaParameter>
    {
        public QueryResult Handle(ListarUsuarioAreaParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idArea", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.CO_AREA);
                var result = new ListarUsuarioAreaResult();
                result.Hits = connection.Query<UsuarioAreaDTO>(
                                    "usp_listar_usuarios_por_area",
                                    parametros,
                                    commandType: CommandType.StoredProcedure );
                return result;
            }
        }
    }
}