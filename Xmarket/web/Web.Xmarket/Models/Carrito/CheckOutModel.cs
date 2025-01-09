using CommandContracts.Xmarket.Cuenta;
using CommandContracts.Xmarket.Pedido;
using QueryContracts.Xmarket.Carrito;
using QueryContracts.Xmarket.Cliente.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Xmarket.Utilitario;

namespace Web.Xmarket.Models.Carrito
{
    public class CheckOutModel : CheckOutCarritoModel
    {

        public string correo { get; set; }
        public bool isSeleccionadoDireccion { get; set; }

        public ClienteDireccionListarResult ClienteDireccionListarResult { get; set; }
        public RegistrarClienteContactoCommand registrarClienteContactoCommand { get; set; }

        public DireccionCarritoModel direccionCarrito { get; set; }

    }
    
}