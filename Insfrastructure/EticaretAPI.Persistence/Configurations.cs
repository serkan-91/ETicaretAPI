using Microsoft.Extensions.Configuration;

namespace EticaretAPI.Persistence;

static class Configurations
{
    private static readonly IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../EticaretAPI.API"))
        .AddJsonFile("appsettings.json")
        .Build();
    public static string ConnectionString =>
        configuration.GetConnectionString("PostgreSQL")
        ?? throw new ArgumentNullException(nameof(ConnectionString));
}
