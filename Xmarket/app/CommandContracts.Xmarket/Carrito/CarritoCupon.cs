using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandContracts.Xmarket.Carrito
{
    public class CarritoCupon
    {

        public int? IdCarrito { get; set; }
        public int? IdCupon { get; set; }
        public decimal? MontoProcentaje { get; set; }
        public double? MontoProcentajeMoney { get; set; }
        public string CodigoCupon { get; set; }
        public string Descripcion { get; set; }
        public int? IdUsuarioCrea { get; set; }
       

    }
}
