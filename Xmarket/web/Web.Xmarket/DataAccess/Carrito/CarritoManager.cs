using QueryContracts.Xmarket.Carrito.Parameters;
using QueryContracts.Xmarket.Carrito.Result;
using ServiceAgents.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Xmarket.DataAccess.Carrito
{
    public class CarritoManager
    {



        private static readonly Lazy<CarritoManager> _instance =
        new Lazy<CarritoManager>(() => new CarritoManager());

        public static CarritoManager Instance
        {
            get { return _instance.Value; }
        }

        public ListarCarritoResult getCarrito(int codigo) {


            ListarCarritoParameter listarCarritoParameter = new ListarCarritoParameter();
            listarCarritoParameter.IdCarrito = codigo;
            ListarCarritoResult result = (ListarCarritoResult)listarCarritoParameter.Execute();

            return result;

        }



    }
}