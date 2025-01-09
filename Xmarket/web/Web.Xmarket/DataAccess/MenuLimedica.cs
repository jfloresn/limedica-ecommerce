
namespace Web.Xmarket.DataAccess
{
    using System.Text;

    using QueryContracts.Xmarket.Account.Parameters;
    using QueryContracts.Xmarket.Account.Results;
    using System;
    using System.Linq;
    using ServiceAgents.Common;
    using log4net;
    using System.Reflection;
    using QueryContracts.Xmarket.Cliente.Parameters;
    using QueryContracts.Xmarket.Cliente.Result;
    using QueryContracts.Xmarket.Editorial.Result;
    using QueryContracts.Xmarket.Especialidad.Result;

    public class MenuLimedica
    {



       public  ListarTodoEditorialResult todoEditorial {get;set;}
        public ListarTodoEspecialidadResult especialidad { get; set; }
        





    }
}