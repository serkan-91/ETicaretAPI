using EticaretAPI.Application.Repositories; 
using EticaretAPI.Persistence.Contexts;
using EticaretAPI.Persistence.Repositories;
using Microsoft.EntityFrameworkCore; 
using Microsoft.Extensions.DependencyInjection; 

namespace EticaretAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        { 
            services.AddDbContext<EticaretAPIDbContext>(options => options.UseNpgsql(Configurations.ConnectionString));
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>(); 
        }
    }
}
