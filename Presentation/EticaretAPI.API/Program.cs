using EticaretAPI.API.Extensions;
using EticaretAPI.Application.Common.MappingProfiles;
using EticaretAPI.Application.Validators.Products;
using EticaretAPI.Infrastructure;
using EticaretAPI.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using static EticaretAPI.Domain.Entities.File;

#region Services Configurations
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();
builder
    .Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>(optional: true)
    .AddEnvironmentVariables();

#region CustomServices
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddStorageService(StorageType.Azure);
#endregion

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowLocalhost4200",
        policy =>
        {
            policy
                .WithOrigins("https://localhost:4200", "http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials(); // Allow cookies and authentication
        }
    );
});

#region FluentValidation Services
// FluentValidation setup
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Create_Product_Validator>();
#endregion

builder.Services.AddControllers();

#region Application Configuration Services
EticaretAPI.Infrastructure.Configurations.SetConfiguration(builder.Configuration);
EticaretAPI.Persistence.Configurations.SetConfiguration(builder.Configuration);
#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAntiforgery(options =>
{
    options.Cookie.Name = ".AspNetCore.Antiforgery.Gnn5bnblBGc"; // Set cookie name
    options.Cookie.SameSite = SameSiteMode.None; // Allow cross-origin requests
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // HTTPS for secure cookies
});

#endregion

#region Application
var app = builder.Build();

#region Environment Configurations - 1
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EticaretAPI v1");
        c.RoutePrefix = string.Empty;
    });
}
else
{
    app.UseHsts();
}
#endregion

#region Protocol Settings - 2
app.UseHttpsRedirection(); // HTTP isteklerini HTTPS'ye yönlendir
app.UseStaticFiles(); // Serve static files
app.UseCors("AllowLocalhost4200"); // Use CORS policy
app.UseRouting(); // Routing request
#endregion

#region Application Security - 3
app.UseAuthentication(); // Authentication access
app.UseAuthorization(); // Authorizations
#endregion
app.UseAntiforgery();
#region Custom Middlewares -4
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<SaveChangesMiddleware>();
#endregion

#region Endpoints and Routing Setup -5
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Routing setup
app.MapProductsEndPoint();
app.MapCrfTokenEndPoint();
#endregion



// Fallback for SPA
app.MapFallbackToFile("index.html");

app.Run();
#endregion
