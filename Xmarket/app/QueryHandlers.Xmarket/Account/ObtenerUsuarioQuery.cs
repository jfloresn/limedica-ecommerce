﻿

namespace QueryHandlers.Xmarket.Account
{
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    using Data.Common;

    using QueryContracts.Common;
    using QueryContracts.Xmarket.Account.Parameters;
    using QueryContracts.Xmarket.Account.Results;

    using QueryHandlers.Common;
    using QueryHandlers.Common.Dapper;
    using System;

    public class ObtenerUsuarioQuery : IQueryHandler<ObtenerUsuarioParameter>
    {
        public QueryResult Handle(ObtenerUsuarioParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("pusr_str_red", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.Usr_str_red);
                parametros.Add("pusr_int_id", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.Usr_int_id);
            
                var parametros2 = new DynamicParameters();
                parametros2.Add("usr_str_red", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.Usr_str_red);
            

                var resultado = connection.Query<ObtenerUsuarioResult>
                  (
                      "seguridad.pa_obtenerusuario",
                       parametros,
                       commandType: CommandType.StoredProcedure
                  ).LastOrDefault();


             
         
                return resultado;
            }
        }
    }
}
