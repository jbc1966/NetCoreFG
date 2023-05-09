using System;
using System.ComponentModel.DataAnnotations;

namespace ControlGastos.Models
{
	public class Categoria
	{
        public int Id { get; set; }

		[Required(ErrorMessage ="El campo {0} es obligatorio")]
		[StringLength(maximumLength:30, ErrorMessage ="No puede ser mayor de {1} caracteres")]
		public string Nombre { get; set; }

		[Display(Name ="Tipo Operación")]
		public TipoOperacion TipoOperacionId { get; set; }
		public int UsuarioId { get; set; }
	}
}

