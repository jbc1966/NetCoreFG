using System;
namespace ControlGastos.Models
{
	public class ReporteTransaccionesDetalladas
	{
		public DateTime FechaInicio { get; set; }
		public DateTime FechaFin { get; set; }
		public IEnumerable<TransaccionesPorFecha> TransaccionesAgrupadas { get; set; }
		public decimal BalanceIngresos => TransaccionesAgrupadas.Sum(x => x.BalanceIngresos);
        public decimal BalanceGastos => TransaccionesAgrupadas.Sum(x => x.BalanceGastos);
		public decimal Total => BalanceIngresos - BalanceGastos;

        public class TransaccionesPorFecha
		{
			public DateTime FechaTransaccion { get; set; }
			public IEnumerable<Transaccion> Transacciones { get; set; }

			public decimal BalanceIngresos => Transacciones.Where(x => x.TipoOperacionId == TipoOperacion.Ingreso)
											  .Sum(x => x.Importe);

            public decimal BalanceGastos => Transacciones.Where(x => x.TipoOperacionId == TipoOperacion.Gasto)
                                              .Sum(x => x.Importe);

			public decimal Total => BalanceIngresos - BalanceGastos;
        }
    }
}

