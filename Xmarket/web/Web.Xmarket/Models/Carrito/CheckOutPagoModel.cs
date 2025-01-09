using CommandContracts.Xmarket.Pedido;
using QueryContracts.Xmarket.Carrito;
using QueryContracts.Xmarket.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Xmarket.Models.Carrito
{
    public class CheckOutPagoModel
    {

        public CarritoDTO carrito { get; set; }

        public RegistrarPedidoCommand registrarPedidoCommand { get; set; }
        public List<ClienteDireccionDTO> direcciones { get; set; }
        public DireccionCarritoModel direccionCarrito { get; set; }
        public SelectList banco { get; set; }
     

    }
}