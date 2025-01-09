using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryContracts.Xmarket.Coleccion
{
    public class ColeccionDTO
    {
        public int idColeccion { get; set; }
        public string nombreColeccion { get; set; }
        public decimal precioColeccion { get; set; }
        public decimal precioDescuentoColeccion { get; set; }
        public bool esDescuento { get; set; }
        public int idEditorial { get; set; }
        public string imagenColeccion { get; set; }
        public string imagenUrlColeccion { get; set; }
        public string EditorialNombre { get; set; }

    }
}
