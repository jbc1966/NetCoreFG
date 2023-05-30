using System;
using System.ComponentModel.DataAnnotations;

namespace ControlGastos.Models
{
	public class RegistroViewModel
	{

		[Required(ErrorMessage = "El campo {0} es obligatorio")]
		[EmailAddress(ErrorMessage = "El campo debe ser un correo electrónico válido")]
		public string Email { get; set; }

		[Required(ErrorMessage = "El campo {0} es obligatorio")]
		public string Password { get; set; }
	}
}

