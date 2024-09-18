using EticaretAPI.API.Extensions;
using EticaretAPI.Application.MappingProfiles;
using EticaretAPI.Application.Validators.Products;
using EticaretAPI.Infrastructure;
using EticaretAPI.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using static EticaretAPI.Domain.Entities.File;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Load configurations
builder.Configuration
	.AddJsonFile("appsettings.json" , optional: true , reloadOnChange: true)
	.AddUserSecrets<Program>(optional: true)
	.AddEnvironmentVariables();

// Set configurations
EticaretAPI.Infrastructure.Configurations.SetConfiguration(builder.Configuration);
EticaretAPI.Persistence.Configurations.SetConfiguration(builder.Configuration);

// Add services to the container
builder.Services.AddAuthorization(); // Add authorization services
builder.Services.AddAuthentication(); // Add your authentication configuration here
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddStorageService(StorageType.Azure);

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configure CORS policy
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowLocalhost4200" , policy =>
	{
		policy.WithOrigins("https://localhost:4200" , "http://localhost:4200")
			  .AllowAnyHeader()
			  .AllowAnyMethod()
			  .AllowCredentials(); // Allow cookies and authentication
	});
});

// FluentValidation setup
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Create_Product_Validator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add antiforgery services
builder.Services.AddAntiforgery(options =>
{
	options.Cookie.Name = ".AspNetCore.Antiforgery.Gnn5bnblBGc"; // Set cookie name
	options.Cookie.SameSite = SameSiteMode.None; // Allow cross-origin requests
	options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // HTTPS for secure cookies
});

// Build the application
var app = builder.Build();
// Development environment configurations
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
app.UseRouting();
app.UseAuthentication(); // Use authentication middleware
app.UseAuthorization(); // Use authorization middleware
app.UseAntiforgery(); // This enables antiforgery protection
app.UseCors("AllowLocalhost4200"); // Use CORS policy

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
});
// Middleware pipeline setup
app.UseHttpsRedirection(); // Redirect HTTP to HTTPS
app.UseStaticFiles(); // Serve static files


// Custom middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<SaveChangesMiddleware>();



// Routing setup
app.MapProductsEndPoint();
app.MapCrfTokenEndPoint();

// Fallback for SPA
app.MapFallbackToFile("index.html");


// Run the application
app.Run();
