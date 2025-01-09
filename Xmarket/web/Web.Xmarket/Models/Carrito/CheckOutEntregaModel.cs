using CommandContracts.Xmarket.Cuenta;
using CommandContracts.Xmarket.Pedido;
using QueryContracts.Xmarket.Carrito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Xmarket.Utilitario;

namespace Web.Xmarket.Models.Carrito
{
    public class CheckOutEntregaModel :CheckOutCarritoModel
    {
    
        public RegistrarClienteSinSesionCommand clienteSinSesionCommand { get; set; }
 


    }
}