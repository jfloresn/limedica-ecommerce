using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Common.Comun
{
    public class ResponseType
    {
        public string Codigo { get; set; }
        public string Mensaje { get; set; }
        public string Estado { get; set; }
        public object Objeto { get; set; }
        
        public object   listObjeto { get; set; }



    }
}
