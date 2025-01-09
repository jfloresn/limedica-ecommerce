using CommandContracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CommandContracts.Xmarket.Carrito
{
    public class EliminarProductoCarritoCommand : Command
    {
       
       
        public long idCarrito { get; set; }
        public int idProducto { get; set; }



    }
}