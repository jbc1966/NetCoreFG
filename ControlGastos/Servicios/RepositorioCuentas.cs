using System;
using ControlGastos.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ControlGastos.Servicios
{
     public interface IRepositorioCuentas
	 {
		Task Crear(Cuenta cuenta);
		Task<IEnumerable<Cuenta>> Buscar(int usuarioId);
		Task<Cuenta> ObtnerPorId(int id, int usuarioId);
		Task Actualizar(CuentaCreacionViewModel cuenta);
		Task Borrar(int id);
     }

	public class RepositorioCuentas: IRepositorioCuentas
	{
		private readonly string connectionString;
        private readonly IConfiguration configuration;

        public RepositorioCuentas(IConfiguration configuration)
		{
			connectionString = configuration.GetConnectionString("DefaultConnection");
            this.configuration = configuration;
        }

		public async Task Crear(Cuenta cuenta)
		{
			using var connection = new SqlConnection(connectionString);
			var id = await connection.QuerySingleAsync<int>(@"Insert into cuentas(Nombre,TipoCuentaId,Descripcion,Balance)
                                                        values(@Nombre,@TipoCuentaId,@Descripcion,@Balance);
                                                        select scope_identity();", cuenta);

			cuenta.Id = id;
		}

		public async Task<IEnumerable<Cuenta>> Buscar(int usuarioId)
		{
			using var connection = new SqlConnection(connectionString);
			return await connection.QueryAsync<Cuenta>(@"select cuentas.id, cuentas.nombre, balance,tc.nombre as TipoCuenta
                                                         from cuentas inner join TiposCuentas tc on tc.Id = cuentas.TipoCuentaId
                                                         where tc.usuarioId = @UsuarioId order by tc.orden", new { usuarioId });


		}

		public async Task<Cuenta> ObtnerPorId(int id, int usuarioId)
		{
			using var connection = new SqlConnection(connectionString);
			return await connection.QueryFirstOrDefaultAsync<Cuenta>(
                @"select cuentas.id, cuentas.nombre, balance,tc.Id,Descripcion
                  from cuentas inner join TiposCuentas tc on tc.Id = cuentas.TipoCuentaId
                  where tc.usuarioId = @UsuarioId and cuentas.Id = @Id", new {id, usuarioId});
		}

		public async Task Actualizar(CuentaCreacionViewModel cuenta)
		{
            using var connection = new SqlConnection(connectionString);
			await connection.ExecuteAsync(
				 @"update cuentas set nombre = @Nombre, balance=@Balance, Descripcion=@Descripcion,
                   TipoCuentaId = @TipoCuentaId where Id=@Id;", cuenta);
        }

		public async Task Borrar(int id)
		{
			using var connection = new SqlConnection(connectionString);
			await connection.ExecuteAsync("delete from cuentas where Id = @Id", new { id });
		}
	}
}

