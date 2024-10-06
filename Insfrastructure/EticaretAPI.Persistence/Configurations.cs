using Microsoft.Extensions.Configuration;

namespace EticaretAPI.Persistence;

public static class Configurations
{
	private static IConfiguration? _configuration;

	public static void SetConfiguration(IConfiguration configuration)
	{
		_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
	}

	// ConnectionString'i almak için bir properti
	public static string ConnectionString =>
		_configuration?.GetConnectionString("PostgreSQL")
		?? throw new InvalidOperationException("PostgreSQL connection string not found.");
	public static string GetCurrentStorage =>
		_configuration?["StorageSettings:Azure"]
		?? throw new InvalidOperationException("Azure Storage connection not found.");
}
