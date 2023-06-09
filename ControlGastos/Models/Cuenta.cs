﻿using System;
using System.ComponentModel.DataAnnotations;
using ControlGastos.Validaciones;
using System.Globalization;


namespace ControlGastos.Models
{

	public class Cuenta
	{
       
		public int Id { get; set; }
		[Required(ErrorMessage="El campo {0} es obligatorio")]
		[StringLength(maximumLength:50)]
		[PrimeraLetraMayuscula]
		public string Nombre { get; set; }
		[Display(Name ="Tipo Cuenta")]
		public int TipoCuentaId { get; set; }

        [DisplayFormat(DataFormatString = "{0:N}",  ApplyFormatInEditMode = true)] //Definir este display format para todos los números decimales 
        public decimal Balance { get; set; }
	   // public string Balance { get; set; }

		[StringLength(maximumLength:200)]
		public string Descripcion { get; set; }
        public string TipoCuenta { get; set; }
	}
}

