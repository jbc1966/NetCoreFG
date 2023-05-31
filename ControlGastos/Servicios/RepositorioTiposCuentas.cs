using System;
using ControlGastos.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ControlGastos.Servicios
{

	public interface IRepositorioTiposCuentas
	{
		Task Crear(TipoCuenta tipoCuenta);
		Task<bool> Existe(string nombre, int UsuarioId, int id = 0);  //Esto se llama llebar la signatura a la interface
		Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId);
		Task<TipoCuenta> ObtenerPorId(int id, int usuarioId);
		Task Actualizar(TipoCuenta tipoCuenta);
		Task Borrar(int id);
		Task Ordenar(IEnumerable<TipoCuenta> tipoCuentasOrdenados);
    }
	public class RepositorioTiposCuentas: IRepositorioTiposCuentas
	{
		private readonly string connectionString;

		public RepositorioTiposCuentas(IConfiguration configuration)
		{
			connectionString = configuration.GetConnectionString("DefaultConnection");

		}

		public async Task Crear(TipoCuenta tipoCuenta)
		{
			try
			{
				tipoCuenta.UsuarioId = 1;
				using var connection = new SqlConnection(connectionString);

				/* Con esto de abajo ataco directamente la base de datos para insertar
				var id = await connection.QuerySingleAsync<int>
													  (@"insert into TiposCuentas (Nombre,UsuarioId,Orden)
                                                  values(@Nombre,@UsuarioId,0);
                                                  select scope_identity();", tipoCuenta);  */

                // Con esto inserto con stored procedure creado en base de datos

				var id = await connection.QuerySingleAsync<int>
					     ("TiposCuentas_Insertar", new
						 {
							 usuarioId = tipoCuenta.UsuarioId,
							 nombre = tipoCuenta.Nombre
						 },
					     commandType: System.Data.CommandType.StoredProcedure);


                tipoCuenta.Id = id;
			}
			catch (Exception ex)
			{
				ex.Message.ToString();
			}
		}

		public async Task<bool> Existe(string nombre, int UsuarioId, int id = 0)
		{
			using var connection = new SqlConnection(connectionString);
			var resultado = await connection.QueryFirstOrDefaultAsync<int>
							(@"select 1 from TiposCuentas
                               where nombre = @Nombre and UsuarioId = @UsuarioId and Id <> @id;",
							   new { nombre, UsuarioId, id});

			return resultado == 1;
		}

        public async Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<TipoCuenta>(
                         @"select Id,Nombre,Orden from TiposCuentas where UsuarioId = @usuarioId Order by Orden",
                         new { usuarioId });
        }

		public async Task Actualizar(TipoCuenta tipoCuenta)
		{
			using var connection = new SqlConnection(connectionString);
			await connection.ExecuteAsync(@"update TiposCuentas set nombre = @Nombre where Id = @Id", tipoCuenta);
		}

		public async Task<TipoCuenta> ObtenerPorId(int id, int usuarioId)
		{
            using var connection = new SqlConnection(connectionString);
		    return await connection.QueryFirstOrDefaultAsync<TipoCuenta>(@"select Id,Nombre,Orden from TiposCuentas
                                               where Id = @id and UsuarioId = @usuarioId", new {id, usuarioId });

        }

        async Task IRepositorioTiposCuentas.Borrar(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("delete tiposcuentas where Id = @Id", new { id });
        }

		public async Task Ordenar(IEnumerable<TipoCuenta> tipoCuentasOrdenados)
		{
			var query = "update TiposCuentas set orden = @Orden where id = @Id;";
			using var connection = new SqlConnection(connectionString);
			await connection.ExecuteAsync(query, tipoCuentasOrdenados);
		  
		}
    }
}

