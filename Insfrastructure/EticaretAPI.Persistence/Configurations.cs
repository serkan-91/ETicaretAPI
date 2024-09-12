using Microsoft.Extensions.Configuration;

namespace EticaretAPI.Persistence;
public static class Configurations
	{
	private static IConfiguration? _configuration;

	// IConfiguration'ı dependency injection ile set et
	public static void SetConfiguration(IConfiguration configuration)
		{
		_configuration = configuration;
		}

	// ConnectionString'i almak için bir properti
	public static string ConnectionString => _configuration.GetConnectionString("PostgreSQL");

	}
