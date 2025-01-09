
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Xmarket.Cliente;
using QueryContracts.Xmarket.Cliente.Parameters;
using QueryContracts.Xmarket.Cliente.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.Xmarket.Categoria
{
    public class ClienteLoginQuery : IQueryHandler<ClienteLoginParameter>
    {

        public QueryResult Handle(ClienteLoginParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {

                var parametros = new DynamicParameters();
                parametros.Add("Correo", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.Correo);
                parametros.Add("Clave", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.Clave);
                parametros.Add("token", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.Token, size: 500);

                parametros.Add("out_codigoresult", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("out_mensaje", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                var result = new ClienteLoginResult();
                result.Hits = connection.Query<ClienteDTO>(
                                    "ecommerce.iniciar_sesion",
                                    parametros,
                                    commandType: CommandType.StoredProcedure);

                result.Estatus.Mensaje = parametros.Get<string>("out_mensaje");
                result.Estatus.CodigoStatus = parametros.Get<Int32>("out_codigoresult");


                return result;

            }

        }

    }
}
