
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
    public class ListarUsuariosQuery : IQueryHandler<ListarUsuariosParameter>
    {
        public QueryResult Handle(ListarUsuariosParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("usr_str_red", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.usr_str_red);
                parametros.Add("usr_str_nombre_apellido", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.usr_str_nombre_apellido);
                parametros.Add("rol_int_id", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.rol_int_id);
                parametros.Add("prm_reginicio", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.registro_inicio);
                parametros.Add("prm_regfin", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.registro_fin);
                parametros.Add("prm_regtotal", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var result = new ListarUsuariosResult();
                result.Hits = connection.Query<ListarUsuariosDto>(
                                    "seguridad.pa_listar_usuarios_filtrar",
                                    parametros,
                                    commandType: CommandType.StoredProcedure );



                result.totalregistros = parametros.Get<System.Int32>("prm_regtotal");

                return result;

            }

        }

       
    }
}
