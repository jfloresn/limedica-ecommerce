using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryContracts.Xmarket.Categoria
{
    public class CategoriaDTO
    {
        public int IdCategoria { get; set; }
        public string IdUp { get; set; }
        public string Stock { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Precio { get; set; }
        public string Peso { get; set; }
        public string Imagen { get; set; }
        public int Orden { get; set; }

    }
}
