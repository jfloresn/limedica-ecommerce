
using System.ComponentModel.DataAnnotations;

namespace Web.Xmarket.Models.Account
{
    public class CambiarPasswordModel
    {


    

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "PasswordNuevoRepetir")]
        public string PasswordNuevoRepetir { get; set; }
    }
}