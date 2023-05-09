using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControlGastos.Models
{
	public class CuentaCreacionViewModel: Cuenta
	{
      public IEnumerable<SelectListItem> TiposCuentas { get; set; }	
	}
}

