
using CommandContracts.Common;
using System;
using System.Collections.Generic;

namespace CommandContracts.Xmarket.Buscar.Output
{
    public class BuscarRegistrarOutput : CommandResult
    {
        public Int32? Estado { get; set; }
        public string Mensaje { get; set; }

        public int? CantidadProductos { get; set; }
    
        
    }
}