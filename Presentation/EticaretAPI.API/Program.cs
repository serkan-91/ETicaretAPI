using EticaretAPI.Application.Validators.Products;
using EticaretAPI.Infrastructure;
using EticaretAPI.Infrastructure.Filters;
using EticaretAPI.Infrastructure.Services.Storages.Local;
using EticaretAPI.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;
using static EticaretAPI.Domain.Entities.File;

var builder = WebApplication.CreateBuilder(args);
// Sadece geliþtirici ortamýnda user-secrets ekleyin
// Konfigürasyon kaynaklarýný ekle
builder.Configuration
	.AddJsonFile("appsettings.json" , optional: true , reloadOnChange: true)
	.AddUserSecrets<Program>(optional: true)
	.AddEnvironmentVariables();

// IConfiguration'ý Configurations sýnýfýna ayarla
EticaretAPI.Infrastructure.Configurations.SetConfiguration(builder.Configuration);
EticaretAPI.Persistence.Configurations.SetConfiguration(builder.Configuration);
// Add configuration sources

// Build the app and continue with setup...
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();

//builder.Services.AddStorage<LocalStorage>();
builder.Services.AddStorageService(StorageType.Azure);

builder.Services.AddCors(options =>
	options.AddDefaultPolicy(policy =>
		policy
			.WithOrigins("http://localhost:4200" , "https://localhost:4200")
			.AllowAnyHeader()
			.AllowAnyMethod()
	)
);

builder
	.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
	.ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Create_Product_Validator>();

var app = builder.Build();

if(app.Environment.IsDevelopment())
	{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json" , "EticaretAPI v1");
		c.RoutePrefix = string.Empty;
	});
	}
else
	{
	app.UseHsts();
	}

app.UseCors();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(name: "default" , pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();