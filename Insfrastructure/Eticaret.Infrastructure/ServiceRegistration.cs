using EticaretAPI.Application.Abstractions.Storage;
using EticaretAPI.Infrastructure.Services.Storage;
using EticaretAPI.Infrastructure.Services.Storage.Local;
using Microsoft.Extensions.DependencyInjection;
using static EticaretAPI.Domain.Entities.File;

namespace EticaretAPI.Infrastructure;

public static class ServiceRegistration
{
	public static void AddInfrastructureServices(this IServiceCollection services)
	{
		services.AddScoped<IStorageService, StorageService>();
	}

	public static void AddStorage<T>(this IServiceCollection services)
		where T : class, IStorage
	{
		services.AddScoped<IStorage, T>();
	}
	public static void AddStorageService (this IServiceCollection services, StorageType storageType)
		{
		IServiceCollection serviceCollection = storageType switch
			{
				StorageType.Local => services.AddScoped<IStorage,LocalStorage> (),
				StorageType.Azure => services.AddScoped<IStorage,LocalStorage> () ,   
				StorageType.AWS =>  services.AddScoped<IStorage,LocalStorage> (),
				_ =>  services.AddScoped<IStorage,LocalStorage> ()
		    };
		}
		}
