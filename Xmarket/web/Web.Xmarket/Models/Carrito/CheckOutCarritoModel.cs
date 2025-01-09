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
    public class CheckOutCarritoModel
    {
        public CarritoDTO carrito { get; set; }

        public SelectList departamentos { get; set; }
        public SelectList provincias { get; set; }
        public SelectList distritos { get; set; }
        public SelectList tipoDocumentosPedido { get; set; }
        public SelectList tipoDocumentos { get; set; }

    }
}