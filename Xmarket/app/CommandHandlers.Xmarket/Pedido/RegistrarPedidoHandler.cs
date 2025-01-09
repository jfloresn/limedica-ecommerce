
using CommandContracts.Common;
using CommandContracts.Xmarket.Carrito.Output;
using CommandContracts.Xmarket.Pedido;
using CommandHandlers.Common;
using Data.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CommandHandlers.Xmarket.Carrito
{
    public class RegistrarPedidoHandler : ICommandHandler<RegistrarPedidoCommand>
    {
        public RegistrarPedidoHandler() { }

        public CommandResult Handle(RegistrarPedidoCommand command)
        {
            var OutPut = new RegistrarPedidoOutput();

            DataTable dtDireccion = new DataTable("TY_DIRECCION");
            dtDireccion.Columns.Add("dire_idireccion", typeof(int));
            dtDireccion.Columns.Add("dire_tipo", typeof(string));

            if (command.direccionClientes != null)
            {
                foreach (DireccionCliente row in command.direccionClientes)
                {
                    DataRow drog = dtDireccion.NewRow();
                    drog["dire_idireccion"] = row.idDireccion;
                    if (row.tipoDireccion == null)
                        drog["dire_tipo"] = DBNull.Value;
                    else
                        drog["dire_tipo"] = row.tipoDireccion;
                    dtDireccion.Rows.Add(drog);
                }
            }

            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("IdCarrito", dbType: DbType.Int64, direction: ParameterDirection.Input, value: command.idCarrito);
                parametros.Add("IdUsuarioLibre", dbType: DbType.Int32, direction: ParameterDirection.Input, value: command.idUsuarioLibre);
                parametros.Add("IdUsuarioIniciaSesion", dbType: DbType.Int32, direction: ParameterDirection.Input, value: command.idUsuarioCrea);
                parametros.Add("IdSesionPublico", dbType: DbType.Int32, direction: ParameterDirection.Input, value: command.sesionPublica);
                parametros.Add("Direccion", dbType: DbType.Object, direction: ParameterDirection.Input, value: dtDireccion);
                parametros.Add("PedidoOrigen", dbType: DbType.String, direction: ParameterDirection.Input, value: command.origenPedido);
                parametros.Add("Estado", dbType: DbType.String, direction: ParameterDirection.Input, value: command.estado);

                parametros.Add("MetodoPago", dbType: DbType.String, direction: ParameterDirection.Input, value: command.metodoPago);
                parametros.Add("BancaInternetBanco", dbType: DbType.Int32, direction: ParameterDirection.Input, value: command.idBanco);
                parametros.Add("BancaInternetMonto", dbType: DbType.Decimal, direction: ParameterDirection.Input, value: command.transferenciaMontoDeposito);
                parametros.Add("BancaInternetFecha", dbType: DbType.Date, direction: ParameterDirection.Input, value: command.transferenciaFechaDeposito);

                parametros.Add("CorreoElectronico", dbType: DbType.String, direction: ParameterDirection.Input, value: command.correo);
                parametros.Add("ComprobantePago", dbType: DbType.String, direction: ParameterDirection.Input, value: command.comprobantePago);
                parametros.Add("TipoDocumento", dbType: DbType.String, direction: ParameterDirection.Input, value: command.tipoDocumento);
                parametros.Add("NumeroDocumento", dbType: DbType.String, direction: ParameterDirection.Input, value: command.numeroDocumento);
                parametros.Add("InfoFacturaIgualEntrega", dbType: DbType.Boolean, direction: ParameterDirection.Input, value: command.informacionFacturaEsIgualEntrega);

                parametros.Add("out_idPedido", dbType: DbType.Int64, direction: ParameterDirection.Output);
                parametros.Add("out_codigoresult", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("out_mensaje", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                connection.Query("ecommerce.sp_pedido_registrar", parametros, commandType: CommandType.StoredProcedure);

                OutPut.Mensaje = parametros.Get<string>("out_mensaje");
                OutPut.Estado = parametros.Get<Int32?>("out_codigoresult");
                OutPut.idPedido = parametros.Get<Int64>("out_idPedido");
            }

            return OutPut;
        }
    }
}