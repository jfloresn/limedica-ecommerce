
using BaseCommon.Common;
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Xmarket.Cliente;
using QueryContracts.Xmarket.Cliente.Parameters;
using QueryContracts.Xmarket.Cliente.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.Xmarket.Categoria
{
    public class ClienteDireccionListarQuery : IQueryHandler<ClienteDireccionListarParameter>
    {

        public QueryResult Handle(ClienteDireccionListarParameter parameters)
        {
            var result = new ClienteDireccionListarResult();

            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {

                var parametros = new DynamicParameters();
                parametros.Add("idUsuario", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idUsuario);
                result.direcciones = connection.Query<ClienteDireccionDTO>(
                                    "ecommerce.cliente_direccion_listar",
                                    parametros,
                                    commandType: CommandType.StoredProcedure);
            }

    
            return result;

        }

    }
}
