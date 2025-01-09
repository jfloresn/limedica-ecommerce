
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Xmarket.Seguridad.Parameters;
using QueryContracts.Xmarket.Seguridad.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace QueryHandlers.Xmarket.Seguridad
{
    public class ObtenerOpcionesQuery : IQueryHandler<ObtenerOpcionesParameter>
    {
        public QueryResult Handle(ObtenerOpcionesParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("CodigoOpcion", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.CodigoOpcion);
 
                var result = new ObtenerOpcionesResult();
                result.Hit = connection.Query<OpcionesDTO>(
                                    "seguridad.usp_web_sel_opciones",
                                    parametros,
                                    commandType: CommandType.StoredProcedure).FirstOrDefault();
                return result;
            }
        }
    }
}