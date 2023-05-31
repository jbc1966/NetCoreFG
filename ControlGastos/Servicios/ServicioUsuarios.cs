using System;
using System.Security.Claims;

namespace ControlGastos.Servicios
{
	public interface IServicioUsuarios
	{
		int ObtenerUsuarioId();
	}

	public class ServicioUsuarios: IServicioUsuarios
	{
		private readonly HttpContext httpContext;

		public ServicioUsuarios(IHttpContextAccessor httpContextAccessor) // Es para poder acceder a los claims del User
		{
			httpContext = httpContextAccessor.HttpContext;
		}


		public int ObtenerUsuarioId()
		{
			if(httpContext.User.Identity.IsAuthenticated)
			{
				var idClaim = httpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
				var id = int.Parse(idClaim.Value);
				return id;
			}
			else
			{
				throw new ApplicationException("Usuario no autenticado");
			}

			return 1;
		}
	}
}

