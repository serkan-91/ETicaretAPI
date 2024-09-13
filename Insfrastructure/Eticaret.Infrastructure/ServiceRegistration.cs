using EticaretAPI.Application.Abstractions.Storage;
using EticaretAPI.Infrastructure.Services.Storage;
using EticaretAPI.Infrastructure.Services.Storages;
using EticaretAPI.Infrastructure.Services.Storages.AWS;
using EticaretAPI.Infrastructure.Services.Storages.Azure;
using EticaretAPI.Infrastructure.Services.Storages.Local;
using Microsoft.Extensions.DependencyInjection;	 
using static EticaretAPI.Domain.Entities.File;

namespace EticaretAPI.Infrastructure;

public static class ServiceRegistration
{
	// Tüm alt sınıflar için IStorageService'i ekliyoruz
	public static void AddInfrastructureServices(this IServiceCollection services)
	{
		// IStorageService kaydını buraya ekleyin
	}

	// Generic bir şekilde IStorage'ı enjekte ediyoruz
	public static void AddStorage<T>(this IServiceCollection services)
		where T : class, IStorage
	{
		services.AddScoped<IStorage, T>();
	}

	// StorageType'a göre IStorage hizmetini kaydediyoruz
	public static void AddStorageService(this IServiceCollection services, StorageType storageType)
	{
		var storeType = storageType switch
		{
			StorageType.Local => typeof(LocalStorage),
			StorageType.Azure => typeof(AzureStorage),
			StorageType.AWS => typeof(AWSStorage),
			_ => throw new ArgumentOutOfRangeException(nameof(storageType), storageType, null),
		};

		services.AddScoped(typeof(IStorage), storeType);

		// StorageType'ı constructor'a geçiyoruz
		services.AddScoped<IStorageService>(provider =>
		{
			var storage = provider.GetRequiredService<IStorage>();
			return new StorageService(storage, storageType);
		});
	}
}
