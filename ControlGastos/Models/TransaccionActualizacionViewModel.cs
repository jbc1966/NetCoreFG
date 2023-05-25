using System;
namespace ControlGastos.Models
{
	public class TransaccionActualizacionViewModel: TransaccionCrearViewModel
	{
		public int CuentaAnteriorId { get; set; }
		public decimal ImporteAnterior { get; set; }
	    public string urlRetorno { get; set; }
	}
}

