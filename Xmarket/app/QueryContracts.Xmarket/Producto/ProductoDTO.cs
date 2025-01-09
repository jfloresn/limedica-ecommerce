using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryContracts.Xmarket.Producto
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }
        public string IdProductoCategoria { get; set; }
        public string IdProductoTxt { get; set; }
        public string IdCategoria { get; set; }
        public string IdCategoriaPadre { get; set; }
        public string IdFabricante { get; set; }
        public string NombrePro { get; set; }
        public string Descripcion { get; set; }
        public bool EsOferta { get; set; }
        public decimal PrecioAnterior { get; set; }
        public decimal Precio { get; set; }
        public string Peso { get; set; }
        public string Imagen { get; set; }
        public string IdAlmacen { get; set; }
        public int Stock { get; set; }
        public string visible { get; set; }
        public int IdEmpresa { get; set; }
        public byte[] Img { get; set; }

        public int Cantidad { get; set; }

        public ProductoCarrito ProductoCarrito { get; set; }

        

    }

}
