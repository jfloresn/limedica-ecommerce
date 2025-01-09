
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
    public class ListarTipoMenuQuery : IQueryHandler<ListarTipoMenuParameter>
    {
        public QueryResult Handle(ListarTipoMenuParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("Estado", dbType: DbType.Boolean, direction: ParameterDirection.Input, value: parameters.Estado);
                
                var result = new ListarTipoMenuResult();
                result.Hits = connection.Query<TipoMenuDTO>(
                                    "dbo.usp_web_sel_tipomenu",
                                    parametros,
                                    commandType: CommandType.StoredProcedure );
                return result;

            }

        }       
    }
}