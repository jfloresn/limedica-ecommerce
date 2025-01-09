using QueryContracts.Xmarket.Carrito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Xmarket.Models.Home;

namespace Web.Xmarket.Models.Carrito
{
    public class CarritoModel
    {

        public  CarritoDTO carrito { get; set; }
        public IEnumerable<ProductoSlide> productoRelacionSlides { get; set; }
    }
}