
using CommandContracts.Common;
using CommandContracts.Xmarket.Carrito;
using CommandContracts.Xmarket.Carrito.Output;
using CommandContracts.Xmarket.Cliente;
using CommandContracts.Xmarket.Cliente.Output;
using CommandContracts.Xmarket.Contactenos;
using CommandContracts.Xmarket.Contactenos.Output;
using CommandHandlers.Common;
using Data.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CommandHandlers.Xmarket.Cliente
{
    public class ContactoHandler : ICommandHandler<ContactenosRegistrarCommand>
    {
        public ContactoHandler()
        {
            
        }

        public CommandResult Handle(ContactenosRegistrarCommand command)
        {          
            var OutPut = new ContactenosRegistrarOutput();
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("cont_nombres", dbType: DbType.String, direction: ParameterDirection.Input, value: command.nombre);
                parametros.Add("cont_apellido", dbType: DbType.String, direction: ParameterDirection.Input, value: command.apellido);
                parametros.Add("cont_correo", dbType: DbType.String, direction: ParameterDirection.Input, value: command.correo);
                parametros.Add("cont_numero", dbType: DbType.String, direction: ParameterDirection.Input, value: command.numeroTelefono);
                parametros.Add("cont_ciudad", dbType: DbType.String, direction: ParameterDirection.Input, value: command.idCiudad);
                parametros.Add("cont_mensaje", dbType: DbType.String, direction: ParameterDirection.Input, value: command.Mensaje);
                parametros.Add("cont_idsesion", dbType: DbType.Int32, direction: ParameterDirection.Input, value: command.idSesion);
                parametros.Add("cont_idsesionpublica", dbType: DbType.Int32, direction: ParameterDirection.Input, value: command.idPublico);
                parametros.Add("cont_idusuariocrea", dbType: DbType.Int32, direction: ParameterDirection.Input, value: command.idUsuarioCrea);
                parametros.Add("cont_asunto", dbType: DbType.String, direction: ParameterDirection.Input, value: command.asunto);
                
                parametros.Add("out_IdContacto", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("out_codigoresult", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("out_mensaje", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

     

                connection.Query
                          (
                              "[ecommerce].[usp_contacto_crear]",
                              parametros,
                              commandType: CommandType.StoredProcedure
                          );

                OutPut.Mensaje = parametros.Get<string>("out_mensaje"); 
                OutPut.Estado = parametros.Get<Int32?>("out_codigoresult");
                OutPut.idContacto = parametros.Get<Int32>("out_IdContacto");
            }

            return OutPut;
        }
    }
}
