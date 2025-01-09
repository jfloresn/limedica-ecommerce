
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
    public class ListarRolesTodoQuery : IQueryHandler<ListarRolesTodoParameter>
    {
        public QueryResult Handle(ListarRolesTodoParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("prm_regtotal", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var result = new ListarRolesTodoResult();
                result.Hits = connection.Query<RolDTO>(
                                    "seguridad.usp_app_sel_rol_todos",
                                    parametros,
                                    commandType: CommandType.StoredProcedure);
                result.totalregistros = parametros.Get<System.Int32>("prm_regtotal");
                return result;
            }
        }
    }
}