namespace EticaretAPI.Infrastructure.Services;

using EticaretAPI.Application.Services;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IFileService, FileService>();
    }
}
