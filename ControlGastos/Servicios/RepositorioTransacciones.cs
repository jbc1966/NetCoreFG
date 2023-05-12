﻿using System;
using ControlGastos.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ControlGastos.Servicios
{
	public interface IRepositorioTransacciones
	{
		Task Crear(Transaccion transaccion);
		Task Actualizar(Transaccion transaccion, decimal importeAnterior, int cuentaAnterior);
		Task<Transaccion> ObtenerPorId(int id, int usuarioId);
		Task Borrar(int id);
		Task<IEnumerable<Transaccion>> ObtenerPorCuentaId(ObtenerTransaccionesPorCuenta modelo);
    }

	public class RepositorioTransacciones: IRepositorioTransacciones
	{
        private readonly string connectionString;
        private readonly IConfiguration configuration;

        public RepositorioTransacciones(IConfiguration configuration)
		{
            connectionString = configuration.GetConnectionString("DefaultConnection");
            this.configuration = configuration;
        }

		public async Task Crear(Transaccion transaccion)
		{
			using var connection = new SqlConnection(connectionString);
			var id = await connection.QuerySingleAsync<int>("Transacciones_insertar",
				     new
					 {
						 transaccion.UsuarioId,
						 transaccion.FechaTransaccion,
						 transaccion.Importe,
						 transaccion.CategoriaId,
						 transaccion.CuentaId,
						 transaccion.Nota
					 }, commandType: System.Data.CommandType.StoredProcedure );  // Aqui voy a usar un procedimiento almacenado

			transaccion.Id = id;

		}

		public async Task Actualizar(Transaccion transaccion, decimal importeAnterior, int cuentaAnteriorId)
		{
			using var connection = new SqlConnection(connectionString);
			await connection.ExecuteAsync("Transacciones_Actualizar",
				new
				{
					transaccion.Id,
					transaccion.FechaTransaccion,
					transaccion.Importe,
					transaccion.CategoriaId,
					transaccion.CuentaId,
					transaccion.Nota,
					importeAnterior,
					cuentaAnteriorId
				}, commandType: System.Data.CommandType.StoredProcedure);
		}

		public async Task<Transaccion> ObtenerPorId(int id, int usuarioId)
		{
			using var connection = new SqlConnection(connectionString);
			return await connection.QueryFirstOrDefaultAsync<Transaccion>(
				@"select transacciones.*, cat.TipoOperacionid
                  from transacciones inner join categorias cat on cat.Id = transacciones.CategoriaId
                  where transacciones.Id = @Id and transacciones.userId = @UsuarioId", new {id, usuarioId});
		}

		public async Task Borrar(int id)
		{
			using var connection = new SqlConnection(connectionString);
			await connection.ExecuteAsync("Transacciones_Borrar",
				new { id }, commandType: System.Data.CommandType.StoredProcedure);
		}

		public async Task<IEnumerable<Transaccion>> ObtenerPorCuentaId(ObtenerTransaccionesPorCuenta modelo)
		{
			using var connection = new SqlConnection(connectionString);
			return await connection.QueryAsync<Transaccion>
				(@"select t.Id, t.Importe, t.FechaTransaccion,c.nombre as categoria, cu.nombre as cuenta, c.tipoOperacionId
                   from transacciones t inner join categorias c on c.Id = t.categoriaId
                   inner join cuentas cu on cu.Id = t.cuentaId where t.cuentaId = @CuentaId and t.UserId = @UsuarioId
                   and fechatransaccion between @FechaInicio and @FechaFin", modelo);

		}
	}
}

