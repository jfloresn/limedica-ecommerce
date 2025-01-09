using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QueryContracts.Xmarket.Banner
{

    
  public  class BannerDTO
    {
        
        public int idBanner { get; set; }
        
        public string autoresBanner { get; set; }
        
        public string tituloBanner { get; set; }
        
        public string descripcionBanner { get; set; }

        public string imagenBanner { get; set; }
        public string tipoBanner { get; set; }

        public string isbnBook { get; set; }
        public bool linkBook { get; set; }
        public string imagenRutaBanner { get; set; }
        public string enlaceBanner { get; set; }
        



    }
}
