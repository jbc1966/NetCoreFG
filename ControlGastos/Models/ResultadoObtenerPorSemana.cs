using System;
namespace ControlGastos.Models
{
	public class ResultadoObtenerPorSemana
	{
		public int Semana { get; set; }
		public decimal Importe { get; set; }
		public TipoOperacion TipoOperacionId { get; set; }
		public decimal Ingresos { get; set; }
		public decimal Gastos { get; set; }
		public DateTime FechaInicio { get; set; }
		public DateTime FechaFin { get; set; }
	}
}

