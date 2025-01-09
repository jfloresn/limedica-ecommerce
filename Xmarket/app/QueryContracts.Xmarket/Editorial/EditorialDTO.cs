using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryContracts.Xmarket.Editorial
{
    public class EditorialDTO
    {
        private string _nombreUrl;

        public int id { get; set; }
        public string nombre { get; set; }
        public string nombreUrl { get {
                if (string.IsNullOrEmpty(_nombreUrl))
                {
                    return this.nombre;
                }
                else {
                    return _nombreUrl;
                }

            } set { _nombreUrl = value; } }

        public string imagen { get; set; }
        public string estadoEditorial { get; set; }
        public string idUsuarioCreaEditorial { get; set; }
        public string idUsuarioModificaEditorial { get; set; }
        public string fechaCreaEditorial { get; set; }
        public string fechaModificaEditorial { get; set; }
        public string ordenEditorial { get; set; }
    }
}
