using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandContracts.Xmarket.Contactenos.Validators
{
  public  class ContactenosRegistrarValidators : AbstractValidator<ContactenosRegistrarCommand>
    {
        public ContactenosRegistrarValidators()
        {
          
            RuleFor(x => x.nombre).NotNull().WithMessage("Se requiere un nombre.").Must(x =>
            {
                string expr = @"^[a-zA-Z\-¿?ÁÉÍÓÚüéáíóúñÑ,¡!:\.\space]*$";

                if (x == null) x = "";

                Regex rgx = new Regex(expr, RegexOptions.IgnoreCase);

                if (rgx.IsMatch(x))
                    return true;
                else
                    return false;

            }).WithMessage("El formato del nombre no es válido.");
            RuleFor(x => x.apellido).NotNull().WithMessage("Se requiere un apellido.");
            RuleFor(x => x.asunto).NotNull().WithMessage("Se requiere un asunto.");
            RuleFor(x => x.correo).NotEmpty().WithMessage("Se requiere un correo.").EmailAddress().WithMessage("Se requiere un correo electrónico válido.");
            RuleFor(x => x.numeroTelefono).NotEmpty().WithMessage("Se requiere el teléfono.").Matches("^[0-9]+$").WithMessage("Formato de telefono invalido.");
            RuleFor(x => x.idCiudad).NotEmpty().WithMessage("Se requiere una ciudad.");
            RuleFor(x => x.Mensaje).NotEmpty().WithMessage("Se requiere un mensaje.");

        }
    }
}