
using CommandContracts.Common;
using CommandContracts.Xmarket.Carrito;
using CommandContracts.Xmarket.Carrito.Output;
using CommandHandlers.Common;
using Data.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CommandHandlers.Xmarket.Carrito
{
    public class CarritoAgregarHandler : ICommandHandler<CarritoAgregarCommand>
    {
        public CarritoAgregarHandler()
        {
            
        }

        public CommandResult Handle(CarritoAgregarCommand command)
        {          
            var OutPut = new CarritoAgregarOutput();

            DataTable dtDetalle = new DataTable("TYPE_CarritoDetalle");
            dtDetalle.Columns.Add("cade_idproducto", typeof(int));
            dtDetalle.Columns.Add("cade_idcarrito", typeof(long));
            dtDetalle.Columns.Add("cade_formato_producto", typeof(string));
            dtDetalle.Columns.Add("cade_nombre_producto", typeof(string));
            dtDetalle.Columns.Add("cade_precio_producto", typeof(decimal));
            dtDetalle.Columns.Add("cade_cantidad_producto", typeof(int));
            dtDetalle.Columns.Add("cade_tipo_accion", typeof(string));
            dtDetalle.Columns.Add("cade_subtotal", typeof(decimal));
            dtDetalle.Columns.Add("cade_imagen", typeof(string));
            dtDetalle.Columns.Add("cade_estado", typeof(string));
            dtDetalle.Columns.Add("cade_idusuario", typeof(int));

            foreach (CarritoDetalle row in command.Carrito.Detalle)
            {
                DataRow drog = dtDetalle.NewRow();

                if (row.IdProducto == null)
                    drog["cade_idproducto"] = DBNull.Value;
                else
                    drog["cade_idproducto"] = row.IdProducto;

                if (row.IdCarrito == null)
                    drog["cade_idcarrito"] = DBNull.Value;
                else
                    drog["cade_idcarrito"] = row.IdCarrito;

                if (row.formatoProducto == null)
                    drog["cade_formato_producto"] = DBNull.Value;
                else
                    drog["cade_formato_producto"] = row.formatoProducto;

                if (row.NombreProducto == null)
                    drog["cade_nombre_producto"] = DBNull.Value;
                else
                    drog["cade_nombre_producto"] = row.NombreProducto;

                if (row.Precio == null)
                    drog["cade_precio_producto"] = DBNull.Value;
                else
                    drog["cade_precio_producto"] = row.Precio;

                if (row.Cantidad == null)
                    drog["cade_cantidad_producto"] = DBNull.Value;
                else
                    drog["cade_cantidad_producto"] = row.Cantidad;

                if (row.TipoAccion == null)
                    drog["cade_tipo_accion"] = DBNull.Value;
                else
                    drog["cade_tipo_accion"] = row.TipoAccion;

                if (row.SubTotal == null)
                    drog["cade_subtotal"] = DBNull.Value;
                else
                    drog["cade_subtotal"] = row.SubTotal;

                if (row.Imagen == null)
                    drog["cade_imagen"] = DBNull.Value;
                else
                    drog["cade_imagen"] = row.Imagen;

                if (row.Estado == null)
                    drog["cade_estado"] = DBNull.Value;
                else
                    drog["cade_estado"] = row.Estado;

                if (row.IdUsuarioCrear == null)
                    drog["cade_idusuario"] = DBNull.Value;
                else
                    drog["cade_idusuario"] = row.IdUsuarioCrear;

                dtDetalle.Rows.Add(drog);
            }

            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("IdCarrito", dbType: DbType.Int64, direction: ParameterDirection.Input, value: command.Carrito.IdCarrito);
                parametros.Add("IdSesion", dbType: DbType.Int32, direction: ParameterDirection.Input, value: command.Carrito.IdSesion);
                parametros.Add("Latitud", dbType: DbType.String, direction: ParameterDirection.Input, value: command.Carrito.Latitud);
                parametros.Add("Longitud", dbType: DbType.String, direction: ParameterDirection.Input, value: command.Carrito.Longitud);
                parametros.Add("IdUsuarioModifica", dbType: DbType.Int32, direction: ParameterDirection.Input, value: command.Carrito.IdUsuarioModificar);
                parametros.Add("EsUsuarioAnonimo", dbType: DbType.Boolean, direction: ParameterDirection.Input, value: command.Carrito.esUsuarioAnonimo);
                parametros.Add("Detalle", dbType: DbType.Object, direction: ParameterDirection.Input, value: dtDetalle);
                parametros.Add("out_Cantidad", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("out_codigoresult", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("out_mensaje", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                OutPut.Observacion = connection.Query<ObservacionCarrito>
                      (
                          "[ecommerce].[usp_carrito_agregar]",
                          parametros,
                          commandType: CommandType.StoredProcedure
                      );

                OutPut.Mensaje = parametros.Get<string>("out_mensaje");
                OutPut.Estado = parametros.Get<Int32?>("out_codigoresult");
                OutPut.CantidadProductos = parametros.Get<Int32?>("out_Cantidad");
            }

            return OutPut;
        }
    }
}
