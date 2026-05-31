using System.Data;

using Dapper;

using Microsoft.Data.SqlClient;

namespace TinyTicket.ApiService;

public class SqlDataAccess
{
	private readonly IConfiguration _config;

	public SqlDataAccess(IConfiguration config)
	{
		_config = config ?? throw new ArgumentNullException(nameof(config));
	}

	public async Task<List<T>> LoadDataAsync<T, U>(
		string storedProcedure,
		U parameters,
		string connectionStringName)
	{

		string connectionString = _config.GetConnectionString(connectionStringName)
															?? throw new InvalidOperationException(
																$"Connection string '{connectionStringName}' was not found.");

		using IDbConnection connection = new SqlConnection(connectionString);

		var rows = (await connection.QueryAsync<T>(
			storedProcedure,
			parameters,
			commandType: CommandType.StoredProcedure)).ToList();

		return rows;

	}


	public async Task SaveDataAsync<T>(
		string storedProcedure,
		T parameters,
		string connectionStringName)
	{

		string connectionString = _config.GetConnectionString(connectionStringName)
															?? throw new InvalidOperationException(
																$"Connection string '{connectionStringName}' was not found.");

		using IDbConnection connection = new SqlConnection(connectionString);

		await connection.ExecuteAsync(
			storedProcedure,
			parameters,
			commandType: CommandType.StoredProcedure);

	}

}
