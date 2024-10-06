using EticaretAPI.Application.Operations;
using EticaretAPI.Domain.Entities.Identity;
using EticaretAPI.Persistence.Operations;
using EticaretAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace EticaretAPI.Persistence;

public static class ServiceRegistration
{
	public static void AddPersistenceServices(this IServiceCollection services)
	{
		services.AddDbContext<EticaretAPIDbContext>(options =>
			options.UseNpgsql(Configurations.ConnectionString)
		);

		services
			.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{
				// Configure identity options
			})
			.AddEntityFrameworkStores<EticaretAPIDbContext>()
			.AddDefaultTokenProviders();

		services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
		services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
		services.AddScoped<IOrderReadRepository, OrderReadRepository>();
		services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
		services.AddScoped<IProductReadRepository, ProductReadRepository>();
		services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
		services.AddScoped<IFileWriteRepository, FileWriteRepository>();
		services.AddScoped<IFileReadRepository, FileReadRepository>();
		services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
		services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();
		services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
		services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
		services.AddScoped<IProductServices, ProductServices>();
	}
}
