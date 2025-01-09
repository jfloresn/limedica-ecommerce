
using CommandContracts.Common;
using CommandContracts.Xmarket.Carrito;
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
    public class RegistrarUsuarioLibreHandler : ICommandHandler<RegistrarClienteSinSesionCommand>
    {
        public RegistrarUsuarioLibreHandler()
        {
        }

        public CommandResult Handle(RegistrarClienteSinSesionCommand command)
        {          
            var OutPut = new RegistrarClienteSinSesionOutput();

            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("uslb_entrega_nombre", dbType: DbType.String, direction: ParameterDirection.Input, value: command.entregaNombre);
                parametros.Add("uslb_entrega_apellido", dbType: DbType.String, direction: ParameterDirection.Input, value: command.entregaApellido);
                parametros.Add("uslb_entrega_direccion", dbType: DbType.String, direction: ParameterDirection.Input, value: command.entregaDireccion);
                parametros.Add("uslb_entrega_referencia", dbType: DbType.String, direction: ParameterDirection.Input, value: command.entregaReferencia);
                parametros.Add("uslb_entrega_departamento", dbType: DbType.String, direction: ParameterDirection.Input, value: command.entregaDepartamento);
                parametros.Add("uslb_entrega_provincia", dbType: DbType.String, direction: ParameterDirection.Input, value: command.entregaProvincia);
                parametros.Add("uslb_entrega_distrito", dbType: DbType.String, direction: ParameterDirection.Input, value: command.entregaDistrito);
                parametros.Add("uslb_contacto_correoelectronico", dbType: DbType.String, direction: ParameterDirection.Input, value: command.contactoCorreoElectronico);
                parametros.Add("uslb_contacto_telefono", dbType: DbType.String, direction: ParameterDirection.Input, value: command.contactoTelefono);
                parametros.Add("uslb_contacto_tipodocumento_pedido", dbType: DbType.String, direction: ParameterDirection.Input, value: command.contactoTipoDocumentoPedido);
                parametros.Add("uslb_contacto_tipodocumento", dbType: DbType.String, direction: ParameterDirection.Input, value: command.contactoTipoDocumento);
                parametros.Add("uslb_contacto_numerodocumento", dbType: DbType.String, direction: ParameterDirection.Input, value: command.contactoNumeroDocumento);
                parametros.Add("uslb_contacto_facturacioninformacion_igual_entrega", dbType: DbType.Boolean, direction: ParameterDirection.Input, value: command.facturacionInfomoIgualEntrega);
                parametros.Add("uslb_sesionpublico", dbType: DbType.String, direction: ParameterDirection.Input, value: command.sesionPublico);

                parametros.Add("out_idusuariolibre", dbType: DbType.Int64, direction: ParameterDirection.Output);
                parametros.Add("out_codigoresult", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("out_mensaje", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                connection.Query("ecommerce.sp_usuariolibre_registrar", parametros, commandType: CommandType.StoredProcedure);

                OutPut.Mensaje = parametros.Get<string>("out_mensaje");
                OutPut.Estado = parametros.Get<Int32?>("out_codigoresult");
                OutPut.idUsuarioLibre = parametros.Get<Int64>("out_idusuariolibre");
            }

            return OutPut;
        }
    }
}
