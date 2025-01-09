using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryContracts.Xmarket.Especialidad
{
    public class EspecialidadDTO
    {
        private string _nombreUrl;
        public int id { get; set; }
        public string nombre { get; set; }
        public string nombreUrl { get { if (string.IsNullOrEmpty(_nombreUrl)) {
                    return this.nombre;
                }
                else {
                    return _nombreUrl;
                }  } set { _nombreUrl = value; } }
        public string estadoEspecialidad { get; set; }
        public int? idUsuarioCreaEspecialidad { get; set; }
        public int? idUsuarioModificaEspecialidad { get; set; }
        public DateTime? fechaCreaEspecialidad { get; set; }
        public DateTime? fechaModificaEspecialidad { get; set; }
        public int? ordenEspecialidad { get; set; }
        public string grupoEspecialidad { get; set; }
        public string imagen { get; set; }


    }
}
