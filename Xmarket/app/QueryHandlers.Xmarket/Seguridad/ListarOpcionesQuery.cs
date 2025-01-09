
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
    public class ListarOpcionesQuery : IQueryHandler<ListarOpcionesParameter>
    {
        public QueryResult Handle(ListarOpcionesParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("CodigoPagina", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.CodigoPagina);
                parametros.Add("Codigo", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.Codigo);
                parametros.Add("NombrePagina", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.NombrePagina);

                parametros.Add("RegistroInicial", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.RegistroInicial);
                parametros.Add("RegistroFinal", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.RegistroFinal);
                parametros.Add("TotalRegistros", dbType: DbType.Int32, direction: ParameterDirection.Output);


                var result = new ListarOpcionesResult();
                result.Hits = connection.Query<OpcionesDTO>(
                                    "seguridad.usp_web_sel_opciones_pagina",
                                    parametros,
                                    commandType: CommandType.StoredProcedure).ToList();

                result.TotalRegistros = parametros.Get<System.Int32>("TotalRegistros");

                return result;
            }
        }
    }
}