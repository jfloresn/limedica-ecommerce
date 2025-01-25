
using CommandContracts.Common;
using CommandContracts.Xmarket.Carrito;
using CommandContracts.Xmarket.Carrito.Output;
using CommandContracts.Xmarket.Cliente;
using CommandContracts.Xmarket.Cliente.Output;
using CommandHandlers.Common;
using Data.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CommandHandlers.Xmarket.Cliente
{
    public class BuscarRegistrarHandler : ICommandHandler<RegistrarDireccionCommand>
    {
        public BuscarRegistrarHandler()
        {
            
        }

        public CommandResult Handle(RegistrarDireccionCommand command)
        {          
            var OutPut = new RegistrarDireccionOutput();
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("USDI_IDUSUARIO", dbType: DbType.Int32, direction: ParameterDirection.Input, value: command.idUsuario);
                parametros.Add("USDI_DIRECCION", dbType: DbType.String, direction: ParameterDirection.Input, value: command.direccion);
                parametros.Add("USDI_UBIGEO_DISTRITO", dbType: DbType.String, direction: ParameterDirection.Input, value: command.ubigeoDistrito);
                parametros.Add("USDI_UBIGEO_PROVINCIA", dbType: DbType.String, direction: ParameterDirection.Input, value: command.ubigeoProvincia);
                parametros.Add("USDI_UBIGEO_DEPARTAMENTO", dbType: DbType.String, direction: ParameterDirection.Input, value: command.ubigeoDepartamento);
                parametros.Add("USDI_IDPAIS", dbType: DbType.String, direction: ParameterDirection.Input, value: command.idPais);
                parametros.Add("USDI_ESTADO", dbType: DbType.Int32, direction: ParameterDirection.Input, value: command.estado);
                parametros.Add("USDI_IDUSUARIO_CREA", dbType: DbType.Int32, direction: ParameterDirection.Input, value: command.idUsuarioCrea);
                parametros.Add("USDI_IDSESION", dbType: DbType.Int32, direction: ParameterDirection.Input, value: command.idSesion);
                parametros.Add("USDI_TIPO_DIRECCION", dbType: DbType.String, direction: ParameterDirection.Input, value: command.tipoDireccion);
                parametros.Add("USDI_NOMBRE_CONTACTO", dbType: DbType.String, direction: ParameterDirection.Input, value: command.nombreContacto);
                parametros.Add("USDI_CELULAR_CONTACTO", dbType: DbType.Int32, direction: ParameterDirection.Input, value: command.celularContacto);
                parametros.Add("OUT_IDDIRECCION", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("OUT_ESTADO", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("OUT_MENSAJE", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
         

                connection.Query
                          (
                              "[ecommerce].[usp_usuario_direccion_registrar]",
                              parametros,
                              commandType: CommandType.StoredProcedure
                          );

                OutPut.Mensaje = parametros.Get<string>("OUT_MENSAJE"); 
                OutPut.Estado = parametros.Get<Int32?>("OUT_ESTADO");
                OutPut.IdUsuarioDireccion = parametros.Get<Int32>("OUT_IDDIRECCION");
            }

            return OutPut;
        }
    }
}
