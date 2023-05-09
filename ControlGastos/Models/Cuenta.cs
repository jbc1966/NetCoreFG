using System;
using System.ComponentModel.DataAnnotations;
using ControlGastos.Validaciones;

namespace ControlGastos.Models
{
	public class Cuenta
	{
		public int Id { get; set; }
		[Required(ErrorMessage="El campo {0} es obligatorio")]
		[StringLength(maximumLength:50)]
		[PrimeraLetraMayuscula]
		public string Nombre { get; set; }
		[Display(Name ="Tipo Cuenta")]
		public int TipoCuentaId { get; set; }
		public decimal Balance { get; set; }
		[StringLength(maximumLength:200)]
		public string Descripcion { get; set; }
        public string TipoCuenta { get; set; }
	}
}

