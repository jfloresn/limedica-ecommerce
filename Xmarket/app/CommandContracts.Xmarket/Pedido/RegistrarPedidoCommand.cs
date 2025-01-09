using CommandContracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CommandContracts.Xmarket.Pedido
{
    public class RegistrarPedidoCommand : Command
    {
        public string metodoPago { get; set; }
        public decimal? transferenciaMontoDeposito { get; set; }
        public DateTime? transferenciaFechaDeposito { get; set; }
        public int idUsuarioRegistrar { get; set; }
        public long idUsuarioLibre { get; set; }
        public string idBanco { get; set; }
        public long idCarrito { get; set; }
        public string origenPedido { get; set; }
        public string estado { get; set; }

        public string correo { get; set; }
        public string comprobantePago { get; set; }
        public string tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public bool informacionFacturaEsIgualEntrega { get; set; }
        

        public List<DireccionCliente> direccionClientes { get; set; }

    }

    public class DireccionCliente
    {
        
        public int idDireccion { get; set; }
    
        public string tipoDireccion { get; set; }

    }

   

}