using System;
using AutoMapper;
using ControlGastos.Models;
using ControlGastos.Servicios;

namespace ControlGastos.Servicios
{
	public class AutoMapperProfiles: Profile
	{
		// Sirve para mapear objetos directamente sin tener que hacerlo por código
 
       public AutoMapperProfiles()
	   {
			CreateMap<Cuenta, CuentaCreacionViewModel>();
			CreateMap<TransaccionActualizacionViewModel, Transaccion>().ReverseMap();
	   }
	}
}

