﻿
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Xmarket.Account.Parameters;
using QueryContracts.Xmarket.Account.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


using QueryContracts.Xmarket.Seguridad.Parameters;
using QueryHandlers.Common;
using QueryContracts.Xmarket.Seguridad.Result;


namespace QueryHandlers.Xmarket.Seguridad
{
    public class EliminarPaginaQuery : IQueryHandler<EliminarPaginaParameter>
    {
        public QueryContracts.Common.QueryResult Handle(EliminarPaginaParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("pag_int_id", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.IdPagina);

                var resultado = new EliminarPaginaResult();
                resultado = connection.Query<EliminarPaginaResult>
                        (
                            "seguridad.pa_eliminarPagina",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();
                return resultado;
            }
        }
    }
}
