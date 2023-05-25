using System;
namespace ControlGastos.Models
{
	public class ReporteSemanalViewModel
	{
		public decimal Ingresos => MovimientosSemanales.Sum(x => x.Ingresos);
		public decimal Gastos => MovimientosSemanales.Sum(x => x.Gastos);
		public decimal Total => Ingresos - Gastos;
		public DateTime FechaReferencia { get; set; }
		public IEnumerable<ResultadoObtenerPorSemana> MovimientosSemanales { get; set; }
	}
}

