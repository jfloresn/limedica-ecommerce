using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryContracts.Xmarket.Book
{
    public class BookFiltroDTO
    {

        public BookFiltroDTO() {
            this.bookTitleUrl = "";
        }

        public int bookCodigo { get; set; }
        public string isbnEBook { get; set; }
        public string bookISBN { get; set; }
        public string bookTitulo { get; set; }
        public string bookTitleUrl { get; set; }
        public string bookEditorialCodigo { get; set; }
        public string bookEdicion { get; set; }
        public string bookAnio { get; set; }
        public int? BookCantidad { get; set; }
        public string editorialNombre { get; set; }
        public string bookAutores { get; set; }
        public double bookPrecio { get; set; }
        public double bookPrecioDescuento { get; set; }
        public bool? bookTieneDescuento { get; set; }
        public string bookImagen { get; set; }
        public string bookIdIdioma { get; set; }
        public string idiomaNombre { get; set; }
        public bool esEbook { get; set; }
        public bool esFisico { get; set; }
        public bool esHibrido { get; set; }
        public string bookImagenFull { get; set; }
        public string nombreEspecialidad { get; set; }
        public string bookImagenSmall { get; set; }


    }
}
