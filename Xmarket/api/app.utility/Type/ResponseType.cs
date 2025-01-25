using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Xmarket.Type
{
    public class ResponseType
    {
        public string prm_codigo { get; set; }
        public string prm_mesaje { get; set; }
        public string prm_estado { get; set; }
        public object Objeto { get; set; }
        public IList<object> ListObjeto { get; set; }
        
        public object Lista { get; set; }



    }
}