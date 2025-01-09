using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace QueryContracts.Xmarket.Book
{
    public class BookDTO : BookFiltroDTO
    {
        private string _indice;
        private string _descripcionLarga;
        private string _descripcionCorta;
        public double precioEBook { get; set; }
        public int IdEspecialidad { get; set; }
        public int idEspecialidadRelacionado { get; set; }
        public int idEditorial { get; set; }
        public string descripcionCorta { get { return HttpUtility.HtmlDecode(_descripcionCorta); } set { _descripcionCorta = value; } }
        public string descripcionLarga { get { return HttpUtility.HtmlDecode(_descripcionLarga); } set { _descripcionLarga = value; } }
        public string indice { get { return HttpUtility.HtmlDecode(_indice); } set { _indice = value; } }
        public int pagina { get; set; }
        public string peso { get; set; }
        public string bookSoloNombreImagen { get; set; }
        public string alto { get; set; }
        public string ancho { get; set; }
        public string largo { get; set; }
        public string encuadernacion { get; set; }
        public bool consultarPrecio { get; set; }
        public string bookSubTitulo { get; set; }
        public string bookMetdataTitulo { get; set; }
        public string bookMetdataDescripcion { get; set; }
        public string bookMetdataKeywords { get; set; }
        public string bookMetdataSiteName { get; set; }

        public string bookImagenFull { get; set; }
        public string bookImagenSmall { get; set; }


    }
}
