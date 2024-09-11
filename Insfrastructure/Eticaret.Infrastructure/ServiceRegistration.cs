using EticaretAPI.Application.Operations;
using EticaretAPI.Application.Repositories;
using EticaretAPI.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EticaretAPI.Persistence;

public static class ServiceRegistration
{
	public static void AddInfrastructureServices(this IServiceCollection services)
	{
		services.AddScoped<IFileService, FileService>();
	}
}
