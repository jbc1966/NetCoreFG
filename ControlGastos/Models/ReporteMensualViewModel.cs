using System;
namespace ControlGastos.Models
{
	public class ReporteMensualViewModel
	{
        public decimal Ingresos => MovimientosMensuales.Sum(x => x.Ingresos);
        public decimal Gastos => MovimientosMensuales.Sum(x => x.Gastos);
        public decimal Total => Ingresos - Gastos;
        public DateTime FechaReferencia { get; set; }
        public IEnumerable<ResultadoObtenerPorMes> MovimientosMensuales { get; set; }
        public int Año { get; set; }
    }
}

