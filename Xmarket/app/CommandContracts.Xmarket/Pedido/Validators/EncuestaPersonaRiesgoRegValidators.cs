using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandContracts.Xmarket.Pedido.Validators
{
  public  class EncuestaPersonaRiesgoRegValidators : AbstractValidator<RegistrarClienteSinSesionCommand>
    {
        public EncuestaPersonaRiesgoRegValidators()
        {
            RuleFor(x => x.entregaNombre).NotNull().WithMessage("Se requiere el nombre");
            RuleFor(x => x.entregaApellido).NotNull().WithMessage("Se requiere el apellido");
            RuleFor(x => x.entregaDireccion).NotNull().WithMessage("Se requiere la dirección");
            RuleFor(x => x.entregaReferencia).NotNull().WithMessage("Se requiere la referencia");
            RuleFor(x => x.entregaDepartamento).NotNull().WithMessage("Se requiere la departamento");
            RuleFor(x => x.entregaProvincia).NotNull().WithMessage("Se requiere la provincia");
            RuleFor(x => x.entregaDistrito).NotNull().WithMessage("Se requiere el distrito");

            RuleFor(x => x.contactoCorreoElectronico).NotNull().WithMessage("Se requiere correo electrónico");
            RuleFor(x => x.contactoCorreoElectronico).EmailAddress().WithMessage("Se requiere correo electrónico válido");
            RuleFor(x => x.contactoTelefono).NotNull().WithMessage("Se requiere el teléfono").Matches("^[0-9]+$").WithMessage("Se pemite telefono");
            

            RuleFor(x => x.contactoTipoDocumentoPedido).NotNull().WithMessage("Se requiere tipo de documento de pedido");
            RuleFor(x => x.contactoTipoDocumento).NotNull().WithMessage("Se requiere tipo de documento de contacto");

            RuleFor(x => x.contactoNumeroDocumento).NotNull().WithMessage("Se requiere el número de documento");

            RuleFor(x => x.entregaNombre).Must(x =>
            {

                string expr = @"^[a-zA-Z\-¿?ÁÉÍÓÚüéáíóúñÑ,¡!:\.\space]*$";

                if (x == null) x = "";

                Regex rgx = new Regex(expr, RegexOptions.IgnoreCase);

                if (rgx.IsMatch(x))
                    return true;
                else
                    return false;

            }).WithMessage("El formato del nombre no es válido");


            RuleFor(x => x.entregaApellido).Must(x =>
            {

                string expr = @"^[a-zA-Z\-¿?ÁÉÍÓÚüéáíóúñÑ,¡!:\.\space]*$";

                if (x == null) x = "";

                Regex rgx = new Regex(expr, RegexOptions.IgnoreCase);

                if (rgx.IsMatch(x))
                    return true;
                else
                    return false;

            }).WithMessage("El formato del apellidos no es válido");


            RuleFor(x => x.facturacionInfomoIgualEntrega).Must(x =>
            {

                if (x == false)

                    return false;

                else
                    return true;

            }).WithMessage("Se requiere aceptar datos de facturación similiar a datos de contacto");
        }
    }
}