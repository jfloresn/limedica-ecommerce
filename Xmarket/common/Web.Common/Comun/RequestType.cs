using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Common.Comun
{
   public  class RequestType
    {
        public string Codigo { get; set; }
        public string Mensaje { get; set; }
        public string Estado { get; set; }
        public object Objeto { get; set; }
        public object listObjeto { get; set; }

    }
}