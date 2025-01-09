
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Xmarket.General.Parameters;
using QueryContracts.Xmarket.General.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.Xmarket.General
{
    public class BuscarTodoQuery : IQueryHandler<BuscarTodoParameter>
    {
        public QueryResult Handle(BuscarTodoParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("TX_FILTRO", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.Filtro);
                parametros.Add("CO_ROL", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.CodigoRol);
                parametros.Add("QL_REGISTRO_INICIO", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.RegistroInicio);
                parametros.Add("QL_REGISTRO_FIN", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.RegistroFin);
                parametros.Add("QL_REGISTROS", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var result = new BuscarTodoResult();
                result.Hits = connection.Query<BuscarTodoDTO>(
                                    "dbo.usp_web_buscar_todo",
                                    parametros,
                                    commandType: CommandType.StoredProcedure);

                result.TotalRegistros = parametros.Get<Int32>("QL_REGISTROS");

                return result;
            }
        }
    }
}