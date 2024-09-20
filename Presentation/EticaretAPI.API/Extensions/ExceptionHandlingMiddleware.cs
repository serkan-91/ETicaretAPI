using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using Npgsql;

namespace EticaretAPI.API.Extensions;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Diğer işlemler...
            await _next(context).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            // Hata yönetimi
            await HandleExceptionAsync(context, ex).ConfigureAwait(false);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        // Hata yanıtı yönetimi
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var result = JsonConvert.SerializeObject(new { error = ex.Message });
        return context.Response.WriteAsync(result);
    }
}
