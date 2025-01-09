using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandContracts.Xmarket.Carrito
{
    public class Carrito
    {
        public int IdCarrito { get; set; }
        public string IdCodSesionPublico { get; set; }
        public int IdSesion { get; set; }
  
        public string IdIpPublica { get; set; }
        public double? TotalPagar { get; set; }
        public int Cantidad { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public DateTime FechaCarrito { get; set; }
        public int IdUsuarioModificar { get; set; }
        public int IdUsuarioCrear { get; set; }
        public DateTime FechaCrea { get; set; }
        public DateTime FechaModifica { get; set; }
        public string Estado { get; set; }
        public int CodigoEstado { get; set; }
        public double? MontoIGV { get; set; }
        public double? MontoDelivery { get; set; }
        public double? MontoSubTotal { get; set; }
        public bool esUsuarioAnonimo { get; set; }
        public IEnumerable<CarritoDetalle> Detalle { get; set; }
        public IEnumerable<CarritoCupon> CarritoCupon { get; set; }
    }
}
