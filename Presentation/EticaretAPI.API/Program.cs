using EticaretAPI.API.Extensions;
using EticaretAPI.Application.MappingProfiles;
using EticaretAPI.Application.Validators.Products;
using EticaretAPI.Infrastructure;
using EticaretAPI.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using static EticaretAPI.Domain.Entities.File;

var builder = WebApplication.CreateBuilder(args);
// Sadece geliþtirici ortamýnda user-secrets ekleyin
builder.Configuration
	.AddJsonFile("appsettings.json" , optional: true , reloadOnChange: true)
	.AddUserSecrets<Program>(optional: true)
	.AddEnvironmentVariables();

EticaretAPI.Infrastructure.Configurations.SetConfiguration(builder.Configuration);
EticaretAPI.Persistence.Configurations.SetConfiguration(builder.Configuration);
// Add configuration sources

// Build the app and continue with setup...
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();

//builder.Services.AddStorage<LocalStorage>();
builder.Services.AddStorageService(StorageType.Azure);

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddCors(options =>
	options.AddDefaultPolicy(policy =>
		policy
			.WithOrigins("http://localhost:4200" , "https://localhost:4200")
			.AllowAnyHeader()
			.AllowAnyMethod()
	)
);

// FluentValidation'ý ekleyin
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
// Tüm doðrulayýcýlarý otomatik olarak taramak ve eklemek için
builder.Services.AddValidatorsFromAssemblyContaining<Create_Product_Validator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add Antiforgery before building the app
builder.Services.AddAntiforgery();
var app = builder.Build();

app.UseAntiforgery();
app.UseMiddleware<ExceptionHandlingMiddleware>();

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
app.MapProductsEndPoint();

app.UseCors();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapFallbackToFile("index.html");

app.Run();
