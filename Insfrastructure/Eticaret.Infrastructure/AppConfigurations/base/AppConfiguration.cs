using EticaretAPI.Infrastructure.AppConfigurations.Azure.Configurations;
using Microsoft.Extensions.Configuration;

namespace EticaretAPI.Infrastructure.AppConfigurations.@base;


public class AppConfiguration
{
    private readonly IConfiguration _configuration;
    public AppConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public AzureStorageConfiguration AzureStorageConfiguration
    {
        get
        {
            return _configuration.GetSection("Storages").Get<AzureStorageConfiguration>();
        }
    }
}

