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
    public class AutorizacionRolQuery : IQueryHandler<AutorizacionRolParameter>
    {

        public AutorizacionRolQuery()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public QueryContracts.Common.QueryResult Handle(AutorizacionRolParameter parameters)
        {
            if (parameters.tipo_usuario == "U")
            {
                using (var connection = (SqlConnection)ConnectionFactory.CreateFromTerminalSession())
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@Nu_Documento", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numero_op_despachador);
                    parametros.Add("@Nu_OrdenRetiro", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numero_orden_retiro);
                    parametros.Add("@Nu_OrdenSalida", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numero_orden_salida);
                    var resultado = new AutorizacionRolResult();
                    resultado = connection.Query<AutorizacionRolResult>
                            (
                                "ValidarAutorizacionORDespachador",
                                parametros,
                                commandType: CommandType.StoredProcedure
                            ).LastOrDefault();
                    return resultado;
                }
            }
            else
            {
                using (var connection = (SqlConnection)ConnectionFactory.CreateFromTerminalSession())
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@Nu_Ruc", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.ruc_agencia);
                    parametros.Add("@Nu_OrdenRetiro", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numero_orden_retiro);
                    parametros.Add("@Nu_OrdenSalida", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numero_orden_salida);
                    var resultado = new AutorizacionRolResult();
                    resultado = connection.Query<AutorizacionRolResult>
                            (
                                "ValidarAutorizacionORAgencia",
                                parametros,
                                commandType: CommandType.StoredProcedure
                            ).LastOrDefault();
                    return resultado;
                }
            }
        }
    }
}