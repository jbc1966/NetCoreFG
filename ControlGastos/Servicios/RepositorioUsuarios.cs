using System;
using ControlGastos.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ControlGastos.Servicios
{

	public interface IRepositorioUsuarios
	{
		Task<int> CrearUsuario(Usuario usuario);
		Task<Usuario> BuscarUsuarioPorEmail(string emailNormalizado);
    }

	public class RepositorioUsuarios: IRepositorioUsuarios
	{
		private readonly string connectionString;

		public RepositorioUsuarios(IConfiguration configuration)
		{
			connectionString = configuration.GetConnectionString("DefaultConnection");
		}

		public async Task<int> CrearUsuario(Usuario usuario)
		{
			using var connection = new SqlConnection(connectionString);

			usuario.EmailNormalizado = usuario.Email.ToUpper();

			var id = await connection.QuerySingleAsync<int>(@"
             INSERT INTO Usuarios (Nombre,Email, EmailNormalizado, PasswordHash)
             VALUES(@Email, @Email, @EmailNormalizado,@PasswordHash)", usuario);

			return id;
		}

		public async Task<Usuario> BuscarUsuarioPorEmail(string emailNormalizado)
		{
			using var connection = new SqlConnection(connectionString);
			return await connection.QuerySingleOrDefaultAsync<Usuario>(@"
                         SELECT * FROM USUARIOS WHERE EMAILNORMALIZADO = @emailNormalizado", new { emailNormalizado });
		}

	}
}

