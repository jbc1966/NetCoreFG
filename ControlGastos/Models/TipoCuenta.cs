using System;
using System.ComponentModel.DataAnnotations;
using ControlGastos.Validaciones;
using Microsoft.AspNetCore.Mvc;

namespace ControlGastos.Models
{
	public class TipoCuenta  //: IValidatableObject
	{
		public int Id { get; set; }

		[Required(ErrorMessage="El campo {0} es obligatorio")]
		// [StringLength(maximumLength:50, MinimumLength = 3, ErrorMessage = "La longitud debe estar entre {3} y {2}")]
		[PrimeraLetraMayuscula]
		[Display(Name ="Nombre de la cuenta")]
		// Añado la validación de que no existe la cuenta sin que el usuario pinche en Enviar
		[Remote(action:"VerificarExisteTipoCuenta", controller:"TiposCuentas", AdditionalFields = nameof(Id))]
		public string Nombre { get; set; }
        public int UsuarioId { get; set; }
		public int Orden { get; set; }

		/*Otros campos que  puedo validar

		[Required(ErrorMessage = "El campo {0} es obligatorio")]
		[EmailAddress(ErrorMessage = "El campo debe ser un correo electrónico válido")]
		public string Email { get; set; }

		[Range(minimum: 18, maximum: 100, ErrorMessage = "El valor debe estar entre {1} y {2}")]
		public int Edad { get; set; }

		[Url(ErrorMessage = "El campo debe ser una URL válida")]
		public string URL { get; set; }

		[CreditCard(ErrorMessage = "La tarjeta de crédito debe ser válida")]
		[Display(Name = "Tarjeta de crédito")]
		public string TarjetaDeCredito { get; set; }


		// Poner IValidatableObject para implementar la validación por modelo
   /*
		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (Nombre != null && Nombre.Length > 0)
			{
				var primeraLetra = Nombre[0].ToString();
				if (primeraLetra != primeraLetra.ToUpper())
				{
					yield return new ValidationResult("La primera letra debe ser mayúscula",
						new[] { nameof( Nombre) });  //Al poner Nombre el error sólo lo dará en el campo Nombre

                    //Error a nivel de modelo general:
                    /*
					  yield return new ValidationResult("La primera letra debe ser mayúscula"); // Esto afectaría a nivel de todo del modelo no de un campo
					 
                }
            }
		}
    */
	}
}

