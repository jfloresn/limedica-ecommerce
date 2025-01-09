
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Xmarket.General.Parameters;
using QueryContracts.Xmarket.General.Result;
using QueryContracts.Xmarket.Producto;
using QueryContracts.Xmarket.Producto.Parameters;
using QueryContracts.Xmarket.Producto.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.Xmarket.Producto
{
    public class ListarProductoPorCategoriaQuery : IQueryHandler<ListarProductoPorCategoriaParameter>
    {
        public QueryResult Handle(ListarProductoPorCategoriaParameter parameters)
        {

            var result = new ListarProductoPorCategoriaResult();


            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("DistribuidorType", dbType: DbType.Object, direction: ParameterDirection.Input, value: parameters.dtDistribuidor);
                parametros.Add("IdCategoria", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.IdCategoria);
                parametros.Add("prm_reginicio", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.RegistroInicio);
                parametros.Add("prm_regfin", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.RegistroFin);
                parametros.Add("prm_regtotal", dbType: DbType.Int32, direction: ParameterDirection.Output);

                
                result.Hits = connection.Query<ProductoDTO>(
                                    "Web_xmarket_producto_listar_categoria",
                                    parametros,
                                    commandType: CommandType.StoredProcedure);


                result.TotalRegistro = parametros.Get<Int32>("prm_regtotal");

                return result;


            }


        }
    }
}
