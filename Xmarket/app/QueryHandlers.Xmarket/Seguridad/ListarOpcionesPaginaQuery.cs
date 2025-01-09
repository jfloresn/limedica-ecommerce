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
    public class ListarOpcionesPaginaQuery : IQueryHandler<ListarOpcionesPaginaParameter>
    {
        public QueryResult Handle(ListarOpcionesPaginaParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("CodigoRol", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.CodigoRol);
                parametros.Add("CodigoPagina", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.CodigoPagina);
                parametros.Add("RegistroInicio", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.RegistroInicio);
                parametros.Add("RegistroFin", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.RegistroFin);
                parametros.Add("TotalRegistro", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var result = new ListarOpcionesPaginasResult();
                result.Hits = connection.Query<OpcionesPaginaDTO>(
                                    "seguridad.usp_web_sel_opcionespagina",
                                    parametros,
                                    commandType: CommandType.StoredProcedure).ToList();

                result.TotalRegistros = parametros.Get<System.Int32>("TotalRegistro");

                return result;
            }
        }
    }
}