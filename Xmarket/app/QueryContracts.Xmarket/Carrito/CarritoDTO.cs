using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryContracts.Xmarket.Carrito
{
    public class CarritoDTO
    {
        public long idCarrito { get; set; }
        public string idSesionPublic { get; set; }
        public int? idSesion { get; set; }
        public IEnumerable<CarritoDetalleDTO> carritoDetalle { get; set; }
        public int? totalDetalleCarrito { get; set; }

        public int? cantidad { get; set; }

        public decimal totalPagar { get; set; }
        public string estado { get; set; }
        public decimal montoIGV { get; set; }
        public decimal montoDelivery { get; set; }
        public decimal montoSubTotal { get; set; }
        public bool esUsuarioAnonimo { get; set; }
        public decimal montoDescuento { get; set; }
        
    }
}
