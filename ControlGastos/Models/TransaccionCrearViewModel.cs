using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControlGastos.Models
{
	public class TransaccionCrearViewModel: Transaccion
	{
		public IEnumerable<SelectListItem> Cuentas { get; set; }
		public IEnumerable<SelectListItem> Categorias { get; set; }

	}
}

