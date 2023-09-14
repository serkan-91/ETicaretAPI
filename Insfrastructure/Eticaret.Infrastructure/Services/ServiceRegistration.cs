using EticaretAPI.Application.Storage;
using EticaretAPI.Infrastructure.Enums;
using EticaretAPI.Infrastructure.Services.Storages;
using EticaretAPI.Infrastructure.Services.Storages.Azure;
using EticaretAPI.Infrastructure.Services.Storages.Local;
using Microsoft.Extensions.DependencyInjection;

namespace EticaretAPI.Infrastructure.Services;
public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IStorageService,StorageService>();
    }

    public static void AddStorage<T>(this IServiceCollection services) where T : Storage, IStorage
    {
        services.AddScoped<IStorage , T>();
    }
    public static void AddStorage<T>(this IServiceCollection services, StorageType storageType)  
    {
        switch (storageType)
        {
            case StorageType.Local:
                services.AddScoped<IStorage, LocalStorage>();
                break;
            case StorageType.Azure:
                services.AddScoped<IStorage , AzureStorage>();
                break;
            case StorageType.AWS:
                break;
            default:
                services.AddScoped<IStorage, LocalStorage>();
                break;
        }
    }
}
