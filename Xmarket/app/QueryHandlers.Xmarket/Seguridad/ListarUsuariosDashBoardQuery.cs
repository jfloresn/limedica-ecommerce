
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
    public class ListarUsuariosDashBoardQuery : IQueryHandler<ListarUsuariosDashBoardParameter>
    {
     
        public QueryResult Handle(ListarUsuariosDashBoardParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {

                var result = new ListarUsuariosDashBoardResult();
                result.Hits = connection.Query<ListarUsuariosDto>(
                                    "dbo.pa_listar_usuarios_dashboard",
                                    null,
                                    commandType: CommandType.StoredProcedure);
                return result;

            }

        }
    }
}
