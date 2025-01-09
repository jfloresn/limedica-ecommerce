

namespace Web.Xmarket.Models.Account
{
    using CommandContracts.Xmarket.Cuenta;
    using CommandContracts.Xmarket.General;
    using QueryContracts.Xmarket.Account.Parameters;
    using QueryContracts.Xmarket.General;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Web.Xmarket.Utilitario;

    public class SignInModel
    {
         public string ReturnUrl { get; set; }
         public  ValidarUsuarioCommand ValidarUsuario {get;set;}

    }
}