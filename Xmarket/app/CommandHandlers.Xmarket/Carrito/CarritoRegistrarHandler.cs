
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
    public class CarritoRegistrarHandler : ICommandHandler<CarritoRegistrarCommand>
    {
        
        public CarritoRegistrarHandler()
        {
            
        }

        public CommandResult Handle(CarritoRegistrarCommand command)
        {

          
            var OutPut = new CarritoRegistrarOutput();

            DataTable dtDetalle = new DataTable("TYPE_CarritoDetalle");
            dtDetalle.Columns.Add("cade_idproducto", typeof(int));
            dtDetalle.Columns.Add("cade_idcarrito", typeof(long));
            dtDetalle.Columns.Add("cade_formato_producto", typeof(string));
            dtDetalle.Columns.Add("cade_nombre_producto", typeof(string));
            dtDetalle.Columns.Add("cade_precio_producto", typeof(double));
            dtDetalle.Columns.Add("cade_cantidad_producto", typeof(int));
            dtDetalle.Columns.Add("cade_tipo_accion", typeof(string));
            dtDetalle.Columns.Add("cade_subtotal", typeof(double));
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

            DataTable dtCupon = new DataTable("TYPE_CarritoCupon");
            dtCupon.Columns.Add("cacu_idcarrito", typeof(long));
            dtCupon.Columns.Add("cacu_idcupon", typeof(int));
            dtCupon.Columns.Add("cacu_monto_porcentaje", typeof(decimal));
            dtCupon.Columns.Add("cacu_monto_porcentaje_money", typeof(decimal));
            dtCupon.Columns.Add("cacu_codigocupon", typeof(string));
            dtCupon.Columns.Add("cacu_descripcion", typeof(string));
            dtCupon.Columns.Add("cacu_idusuario", typeof(int));
     
            if (command.Carrito.CarritoCupon != null)
            {
                foreach (CarritoCupon itemVentaCupon in command.Carrito.CarritoCupon)
                {
                    DataRow drog = dtCupon.NewRow();

                    if (itemVentaCupon.IdCarrito == null)
                        drog["cacu_idcarrito"] = DBNull.Value;
                    else
                        drog["cacu_idcarrito"] =  itemVentaCupon.IdCarrito;

                    if (itemVentaCupon.IdCupon == null)
                        drog["cacu_idcupon"] = DBNull.Value;
                    else
                        drog["cacu_idcupon"] = itemVentaCupon.IdCupon;

                    if (itemVentaCupon.MontoProcentaje == null)
                        drog["cacu_monto_porcentaje"] = DBNull.Value;
                    else
                        drog["cacu_monto_porcentaje"] = itemVentaCupon.MontoProcentaje;

                    if (itemVentaCupon.MontoProcentajeMoney == null)
                        drog["cacu_monto_porcentaje_money"] = DBNull.Value;
                    else
                        drog["cacu_monto_porcentaje_money"] = itemVentaCupon.MontoProcentajeMoney;

                    if (itemVentaCupon.CodigoCupon == null)
                        drog["cacu_codigocupon"] = DBNull.Value;
                    else
                        drog["cacu_codigocupon"] = itemVentaCupon.CodigoCupon;


                    if (itemVentaCupon.Descripcion == null)
                        drog["cacu_descripcion"] = DBNull.Value;
                    else
                        drog["cacu_descripcion"] = itemVentaCupon.Descripcion;

                    if (itemVentaCupon.IdUsuarioCrea == null)
                        drog["cacu_idusuario"] = DBNull.Value;
                    else
                        drog["cacu_idusuario"] = itemVentaCupon.IdUsuarioCrea;
      
          
                    dtCupon.Rows.Add(drog);
                }
            }
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();

                parametros.Add("IdCarrito", dbType: DbType.Int64, direction: ParameterDirection.Input, value: command.Carrito.IdCarrito);
                parametros.Add("IdCodSesionPublic", dbType: DbType.String, direction: ParameterDirection.Input, value: command.Carrito.IdCodSesionPublico);
                parametros.Add("IdSesion", dbType: DbType.Int32, direction: ParameterDirection.Input, value: command.Carrito.IdSesion);
                parametros.Add("IpPublica", dbType: DbType.String, direction: ParameterDirection.Input, value: command.Carrito.IdIpPublica);
                parametros.Add("TotalPagar", dbType: DbType.Decimal, direction: ParameterDirection.Input, value: command.Carrito.TotalPagar);
                parametros.Add("Cantidad", dbType: DbType.Int32, direction: ParameterDirection.Input, value: command.Carrito.Cantidad);
                parametros.Add("Latitud", dbType: DbType.String, direction: ParameterDirection.Input, value: command.Carrito.Latitud);
                parametros.Add("Longitud", dbType: DbType.String, direction: ParameterDirection.Input, value: command.Carrito.Longitud);
                parametros.Add("IdUsuarioCrea", dbType: DbType.Int32, direction: ParameterDirection.Input, value: command.Carrito.IdUsuarioCrear);
                parametros.Add("IdUsuarioModifica", dbType: DbType.Int32, direction: ParameterDirection.Input, value: command.Carrito.IdUsuarioModificar);
                parametros.Add("Estado", dbType: DbType.String, direction: ParameterDirection.Input, value: command.Carrito.Estado);
                parametros.Add("MontoIGV", dbType: DbType.Decimal, direction: ParameterDirection.Input, value: command.Carrito.MontoIGV);
                parametros.Add("MontoDelivery", dbType: DbType.Decimal, direction: ParameterDirection.Input, value: command.Carrito.MontoDelivery);
                parametros.Add("MontoSubTotal", dbType: DbType.Decimal, direction: ParameterDirection.Input, value: command.Carrito.MontoSubTotal);
                parametros.Add("EsUsuarioAnonimo", dbType: DbType.Boolean, direction: ParameterDirection.Input, value: command.Carrito.esUsuarioAnonimo);
                parametros.Add("Detalle", dbType: DbType.Object, direction: ParameterDirection.Input, value: dtDetalle);
                parametros.Add("Cupones", dbType: DbType.Object, direction: ParameterDirection.Input, value: dtCupon);
                parametros.Add("out_Cantidad", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("out_CodigoCarrito", dbType: DbType.Int64, direction: ParameterDirection.Output);
                parametros.Add("out_codigoresult", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("out_mensaje", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                OutPut.Observacion = connection.Query<ObservacionCarrito>
                      (
                          "[ecommerce].[usp_carrito_crear]",
                          parametros,
                          commandType: CommandType.StoredProcedure
                      );

                OutPut.CantidadProductos = parametros.Get<Int32>("out_Cantidad");
                OutPut.IdCarrito = parametros.Get<long?>("out_CodigoCarrito");
                OutPut.Estado = parametros.Get<Int32>("out_codigoresult");
                OutPut.Mensaje = parametros.Get<string>("out_mensaje");
                
                
                
            }

            return OutPut;
        }
    }
}
