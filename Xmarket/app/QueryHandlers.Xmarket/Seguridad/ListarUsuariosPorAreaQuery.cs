
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
    public class ListarUsuariosPorAreaQuery : IQueryHandler<ListarUsuariosPorAreaParameter>
    {
        public QueryResult Handle(ListarUsuariosPorAreaParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("COD_AREA", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.CO_AREA);
                parametros.Add("prm_reginicio", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.prm_reginicio);
                parametros.Add("prm_regfin", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.prm_regfin);
                parametros.Add("prm_regtotal", dbType: DbType.Int32, direction: ParameterDirection.Output);
                


                var result = new ListarUsuariosPorAreaResult();
                result.Hits = connection.Query<ListarUsuariosAreaDto>(
                                    "dbo.usp_web_usuarios_x_area",
                                    parametros,
                                    commandType: CommandType.StoredProcedure );

                result.total_registro = parametros.Get<int>("prm_regtotal");
                return result;
            }

        }
    }
}
