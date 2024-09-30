using System.Text;
using EticaretAPI.API.Extensions;
using EticaretAPI.Application;
using EticaretAPI.Application.Common.MappingProfiles;
using EticaretAPI.Application.Features.Commands;
using EticaretAPI.Application.Validators.Products;
using EticaretAPI.Infrastructure;
using EticaretAPI.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using static EticaretAPI.Domain.Entities.File;

#region Services Configurations
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();
builder
    .Configuration.AddJsonFile("appsettings.json" , optional: true , reloadOnChange: true)
    .AddUserSecrets<Program>(optional: true)
    .AddEnvironmentVariables();

#region CustomServices
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddStorageService(StorageType.Azure);
builder.Services.AddApplicationServices();
#endregion

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddCors(options =>
{
options.AddPolicy(
    "AllowLocalhost4200" ,
    policy =>
    {
    policy
            .WithOrigins("https://localhost:4200" , "http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // Allow cookies and authentication
    }
);
});

#region FluentValidation Services
// FluentValidation setup

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductCommandRequestValidator>();
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
c.SwaggerEndpoint("/swagger/v1/swagger.json" , "EticaretAPI v1");
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

app.Use(
    async (context , next) =>
    {
    context.Request.EnableBuffering();
    var buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];
    await context.Request.Body.ReadAsync(buffer , 0 , buffer.Length);
    var requestBody = Encoding.UTF8.GetString(buffer);
    context.Request.Body.Position = 0;

    Console.WriteLine($"Request Body: {requestBody}");

    await next.Invoke();
    }
);

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
