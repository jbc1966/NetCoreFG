using System;
using ControlGastos.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ControlGastos.Servicios
{
	public interface IRepositorioCategorias
	{
		Task Crear(Categoria categoria);
		Task<IEnumerable<Categoria>> Obtener(int usuarioId, PaginacionViewModel paginacion);
		Task<Categoria> ObtenerPorId(int id, int usuarioId);
		Task Actualizar(Categoria categoria);
		Task Borrar(int id);
		Task<IEnumerable<Categoria>> Obtener(int usuarioId, TipoOperacion tipoOperacionId);
		Task<int> Contar(int usuarioId);
    }

	public class RepositorioCategorias: IRepositorioCategorias
	{
        private readonly string connectionString;

        public RepositorioCategorias(IConfiguration configuration)
		{
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

		public async Task Crear(Categoria categoria)
		{
			using var connection = new SqlConnection(connectionString);
			var id = await connection.QuerySingleAsync<int>(@"
                     insert into categorias (Nombre, TipoOperacionId,UsuarioId)
                     values(@Nombre,@TipoOperacionId,@UsuarioId);
                     select scope_identity();
                     ", categoria);

			categoria.Id = id;
		}

		public async Task<IEnumerable<Categoria>> Obtener(int usuarioId, PaginacionViewModel paginacion)
		{
			using var connection = new SqlConnection(connectionString);
			return await connection.QueryAsync<Categoria>(
				@$"select * from categorias where UsuarioId = @usuarioId
                  ORDER BY Nombre
                  OFFSET {paginacion.RecordsASaltar} ROWS FETCH NEXT {paginacion.RecordsPorPagina}
                  ROWS ONLY", new { usuarioId });

		}

		public async Task<int> Contar(int usuarioId)
		{
            using var connection = new SqlConnection(connectionString);

			return await connection.ExecuteScalarAsync<int>(
				 "select count(*) from categorias where usuarioId = @usuarioId", new {usuarioId});
        }

        public async Task<IEnumerable<Categoria>> Obtener(int usuarioId, TipoOperacion tipoOperacionId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Categoria>(
				@"select * from categorias
                  where UsuarioId = @usuarioId
                  and TipoOperacionId = @tipoOperacionId", new { usuarioId, tipoOperacionId});

        }

        public async Task Borrar(int id)
		{
			using var connection = new SqlConnection(connectionString);
			await connection.ExecuteAsync("delete from categorias where Id = @id", new { id });
		}

		public async Task<Categoria> ObtenerPorId(int id, int usuarioId)
		{
            using var connection = new SqlConnection(connectionString);
		    return await connection.QueryFirstOrDefaultAsync<Categoria>(
				            @"select * from categorias where Id = @Id and UsuarioId = @usuarioId",
							new {id, usuarioId});
        }

		public async Task Actualizar(Categoria categoria)
		{
            using var connection = new SqlConnection(connectionString);
			await connection.ExecuteAsync(@"update categorias set Nombre = @Nombre, TipoOperacionId = @TipoOperacionId
                                          where Id = @Id", categoria);
        }

		
    }
}

