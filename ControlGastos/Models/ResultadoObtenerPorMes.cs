using System;
namespace ControlGastos.Models
{
	public class ResultadoObtenerPorMes
	{
        public int Mes { get; set; }
        public decimal Importe { get; set; }
        public TipoOperacion TipoOperacionId { get; set; }
        public decimal Ingresos { get; set; }
        public decimal Gastos { get; set; }
        public DateTime FechaReferencia { get; set; }
    }
}

