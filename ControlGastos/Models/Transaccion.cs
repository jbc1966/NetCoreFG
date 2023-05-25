using System;
using System.ComponentModel.DataAnnotations;

namespace ControlGastos.Models
{
	public class Transaccion
	{
		public int Id { get; set; }
		public int UsuarioId { get; set; }

		[Display(Name = "Fecha Transacción")]
		[DataType(DataType.DateTime)]
		public DateTime FechaTransaccion { get; set; } = DateTime.Parse(DateTime.Now.ToString("G"));
        [RegularExpression("^[0-9]{1.3}(.[0-9]{3})*\\,[0-9]+$")]
        public decimal Importe { get; set; }

		[Display(Name = "Categoria")]
		[Range(1, maximum: int.MaxValue, ErrorMessage = "Debe seleccionar una categoría")]
		public int CategoriaId { get; set; }

		[StringLength(maximumLength: 150, ErrorMessage = "La nota no puede ser mayor de {1} caracteres")]
		public string Nota { get; set; }

		[Display(Name = "Cuenta")]
		[Range(1, maximum: int.MaxValue, ErrorMessage = "Debe seleccionar una cuenta")]
		public int CuentaId { get; set; }

		[Display(Name = "Tipo Operación")]
		public TipoOperacion TipoOperacionId { get; set; } = TipoOperacion.Ingreso;

		public string Cuenta { get; set; }
		public string Categoria { get; set; }
	}
}

