using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandContracts.Xmarket.Cliente.Validators
{
  public  class RegistrarDireccionValidators : AbstractValidator<RegistrarDireccionCommand>
    {
        public RegistrarDireccionValidators()
        {
            RuleFor(x => x.nombreContacto)
                .NotNull().WithMessage("Se requeire el nombre del contacto")
                .Must(x =>
                {

                    string expr = @"^[a-zA-Z\-¿?ÁÉÍÓÚüéáíóúñÑ,¡!:\.\space]*$";

                    if (x == null) x = "";

                    Regex rgx = new Regex(expr, RegexOptions.IgnoreCase);

                    if (rgx.IsMatch(x))
                        return true;
                    else
                        return false;

                }).WithMessage("El formato del nombre no es válido");
            RuleFor(x => x.celularContacto).NotNull().WithMessage("Se requiere el celular de contacto.");
            RuleFor(x => x.direccion).NotNull().WithMessage("Se requiere la dirección.");
            RuleFor(x => x.ubigeoDepartamento).NotNull().WithMessage("Se requiere la departamento");
            RuleFor(x => x.ubigeoProvincia).NotNull().WithMessage("Se requiere la provincia");
            RuleFor(x => x.ubigeoDistrito).NotNull().WithMessage("Se requiere el distrito");

            


        }
    }
}