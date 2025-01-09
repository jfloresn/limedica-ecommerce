namespace SWeb.Xmarket.Utilitario
{
    using QueryContracts.Xmarket.Book;
    using QueryContracts.Xmarket.Book.Result;
    using QueryContracts.Xmarket.Coleccion;
    using QueryContracts.Xmarket.Coleccion.Result;
    using QueryContracts.Xmarket.Seguridad.Result;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using Web.Xmarket.Models.Coleccion;
    using Web.Xmarket.Models.Home;

    public class Utilitarios
    {


        public string[] split(string p_respuesta)
        {
            string[] respuesta_arreglo;
            string separador = "SP :";
            bool separador_ubicado=false;
            int i=0;
            int j=0;

            respuesta_arreglo=new string[2];

            for (i = 0; i < p_respuesta.Length; i++)
            {
                int _longitud=4;

                if ((j+4)>p_respuesta.Length)
                {
                    _longitud = p_respuesta.Length - j;
                }


                string _cadena = p_respuesta.Substring(j, _longitud);
                if (_cadena == separador)
                {
                    respuesta_arreglo[0] = p_respuesta.Substring(0, j);
                    respuesta_arreglo[1] = p_respuesta;
                    separador_ubicado = true;
                    break;
                }
                j++;
            }
            if(!separador_ubicado)
            {
                respuesta_arreglo[0] = p_respuesta;
                respuesta_arreglo[1] = p_respuesta;

            }
          
            return respuesta_arreglo;
        }
     

        public List<UsuarioAreaDTO> UsuarioAreaSelect(IEnumerable<UsuarioAreaDTO> hist)
        {
            List<UsuarioAreaDTO> Empresas = hist.ToList();
            Empresas.Add(new UsuarioAreaDTO { CO_USUARIO = 0, USUARIO = "[Seleccionar]" });
            return Empresas;
        }

        public List<ProductoSlide> obtenerProductSlide(IEnumerable<BookFiltroDTO> libros, double cantidadProductoPorSlide = 3.00){
            List<ProductoSlide> slides = new List<ProductoSlide>();
 
            double calcularCantidadSlide = (libros.Count() / cantidadProductoPorSlide);
            int cantidadSlide = Convert.ToInt32( Math.Ceiling(calcularCantidadSlide));


            int jPosicionActualProducto = 0;

            for (int iCorrelativoSlide = 0; 
                iCorrelativoSlide < cantidadSlide;
                iCorrelativoSlide++)
            {
                ProductoSlide slide = new ProductoSlide();
                slide.esVisible = iCorrelativoSlide == 0;

               int cantidadMaximaProductoPorSlide = iCorrelativoSlide == (cantidadSlide-1) ? libros.Count() : (Convert.ToInt32(cantidadProductoPorSlide) * (iCorrelativoSlide+1)) ;

                for (int jCorrelativoProducto = jPosicionActualProducto; 
                    jCorrelativoProducto < cantidadMaximaProductoPorSlide; 
                    jCorrelativoProducto++)
                {
                    slide.books.Add(libros.ElementAt(jCorrelativoProducto));

                    jPosicionActualProducto++;
                }
                slides.Add(slide);

            }

            return slides;
        }

        public List<ColeccionSlide> obtenerColeccionSlide(IEnumerable<ColeccionDTO> colecciones, double cantidadProductoPorSlide = 3.00)
        {
            List<ColeccionSlide> slides = new List<ColeccionSlide>();

            double calcularCantidadSlide = (colecciones.Count() / cantidadProductoPorSlide);
            int cantidadSlide = Convert.ToInt32(Math.Ceiling(calcularCantidadSlide));

        
            int jPosicionActualProducto = 0;

            for (int iCorrelativoSlide = 0;
                iCorrelativoSlide < cantidadSlide;
                iCorrelativoSlide++)
            {
                ColeccionSlide slide = new ColeccionSlide();
                slide.esVisible = iCorrelativoSlide == 0;

                int cantidadMaximaProductoPorSlide = iCorrelativoSlide == (cantidadSlide - 1) ? colecciones.Count() : (Convert.ToInt32(cantidadProductoPorSlide) * (iCorrelativoSlide + 1));

                for (int jCorrelativoProducto = jPosicionActualProducto;
                    jCorrelativoProducto < cantidadMaximaProductoPorSlide;
                    jCorrelativoProducto++)
                {
                    slide.colecciones.Add(colecciones.ElementAt(jCorrelativoProducto));

                    jPosicionActualProducto++;
                }
                slides.Add(slide);

            }

            return slides;
        }

  

    }
}
