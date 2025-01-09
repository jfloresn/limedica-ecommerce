
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
    public class ListarRolesQuery : IQueryHandler<ListarRolesParameter>
    {


        public QueryResult Handle(ListarRolesParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("rol_str_alias", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.rol_str_alias);
                parametros.Add("prm_reginicio", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.registro_inicio);
                parametros.Add("prm_regfin", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.registro_fin);
                parametros.Add("prm_regtotal", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var result = new ListarRolesResult();
                result.Hits = connection.Query<RolDTO>(
                                    "seguridad.usp_app_sel_rol_filtrar",
                                    parametros,
                                    commandType: CommandType.StoredProcedure);
                result.totalregistros = parametros.Get<System.Int32>("prm_regtotal");
                return result;
            }
        }
    }
}
