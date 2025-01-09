using QueryContracts.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace QueryContracts.Xmarket.Carrito.Parameters
{
    public class LeerCarritoDetalleParameter : QueryParameter
    {

     
        public long IdCarrito { get; set; }
        public int IdProducto { get; set; }
        
    }
}
