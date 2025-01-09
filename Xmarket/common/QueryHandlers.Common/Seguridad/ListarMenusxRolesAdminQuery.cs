using Data.Common;
using QueryContracts.Common;
using QueryContracts.Common.Seguridad.Parameters;
using QueryContracts.Common.Seguridad.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;


namespace QueryHandlers.Common.Seguridad
{
    public class ListarMenusxRolesAdminQuery : IQueryHandler<ListarMenusxRolesAdminParameter>
    {
        public QueryResult Handle(ListarMenusxRolesAdminParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("rol_int_id", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.rol_int_id);
                parametros.Add("sis_str_siglas", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.sis_str_siglas);

                var result = new ListarMenusxRolesAdminResult();
                result.Hits = connection.Query<MenusxRolesDTO>(
                                    "seguridad.pa_listar_menus_admin",
                                    parametros,
                                    commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}