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
    public class ListarAutorizadorPorAreaQuery : IQueryHandler<ListarAutorizadorAreaParameter>
    {
        public QueryResult Handle(ListarAutorizadorAreaParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idArea", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.CO_AREA);
                parametros.Add("IdRolAutorizador", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.CO_ROL);
                
                var result = new ListarAutorizadorPorAreaResult();
                result.Hits = connection.Query<AutoriadorAreaDTO>(
                                    "usp_listar_autorizador_por_area",
                                    parametros,
                                    commandType: CommandType.StoredProcedure );
                return result;
            }
        }
    }
}