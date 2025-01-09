
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
    public class ListarRolesDashBoardQuery : IQueryHandler<ListarRolesDashBoardParameter>
    {
      

        public QueryResult Handle(ListarRolesDashBoardParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
               
                var result = new ListarRolesDashBoardResult();
                result.Hits = connection.Query<RolDTO>(
                                    "dbo.pa_listar_roles_dashboard",
                                    null,
                                    commandType: CommandType.StoredProcedure);
                return result;
            }
        }
 
    }
}
