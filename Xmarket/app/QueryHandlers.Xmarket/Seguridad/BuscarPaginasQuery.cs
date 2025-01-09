

using Data.Common;
using QueryContracts.Xmarket.Seguridad.Parameters;
using QueryContracts.Xmarket.Seguridad.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace QueryHandlers.Xmarket.Seguridad
{
    public class BuscarPaginasQuery : IQueryHandler<BuscarPaginasParameter>
    {

        public QueryContracts.Common.QueryResult Handle(BuscarPaginasParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("pag_str_codmenu", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.pag_str_codmenu);
                parametros.Add("pag_str_nombre", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.pag_str_nombre);
                parametros.Add("pag_str_codmenupadre", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.CodigoMenuPadre);
                
                parametros.Add("prm_reginicio", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.registro_inicio);
                parametros.Add("prm_regfin", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.registro_fin);
                parametros.Add("prm_regtotal", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var result = new BuscarPaginasResult();
                result.Hits = connection.Query<BuscarPaginasDto>(
                                    "dbo.pa_buscarpaginas_filtrar",
                                    parametros,
                                    commandType: CommandType.StoredProcedure).ToList();
                result.totalregistros = parametros.Get<System.Int32>("prm_regtotal");

                return result;
            }
        }
    }
}
