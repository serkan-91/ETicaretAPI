using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Npgsql;

namespace EticaretAPI.API.Extensions;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger
    )
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
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
        // Varsayılan hata yanıtı
        var statusCode = HttpStatusCode.InternalServerError;
        var errorMessage = "An unexpected error occurred.";
        var errorDetail = ex.Message; // Daha ayrıntılı bilgi
        var stackTrace = ex.StackTrace; // StackTrace detayı

        // Hata türüne göre özel durumlar
        switch (ex)
        {
            case NpgsqlException npgsqlEx:
                statusCode = HttpStatusCode.ServiceUnavailable; // 503
                errorMessage = "Database connection error.";
                _logger.LogError($"Database error: {npgsqlEx.Message}"); // Veritabanı hatalarını logla
                break;

            case SocketException socketEx:
                statusCode = HttpStatusCode.BadGateway; // 502
                errorMessage = "Network error occurred. Unable to resolve DNS.";
                _logger.LogError($"Socket error: {socketEx.Message}"); // DNS veya ağ hatasını logla
                break;

            case ArgumentNullException argNullEx:
                statusCode = HttpStatusCode.BadRequest; // 400
                errorMessage = "A required parameter was missing.";
                _logger.LogError($"Argument null error: {argNullEx.Message}"); // Doğrulama hatasını logla
                break;

            case ArgumentException argEx:
                statusCode = HttpStatusCode.BadRequest; // 400
                errorMessage = "Invalid argument provided.";
                _logger.LogError($"Argument error: {argEx.Message}"); // Yanlış argüman logla
                break;

            default:
                // Genel hata için loglama
                _logger.LogError($"An unexpected error occurred: {ex.Message}");
                break;
        }

        // Hata detaylarını müşteriye dönmek için JSON formatında döndürüyoruz
        var errorResponse = new
        {
            statusCode = (int)statusCode,
            message = errorMessage,
            detail = errorDetail,
            trace = stackTrace // İsteğe bağlı: Trace bilgilerini dönmek riskli olabilir, bunu kaldırabilirsiniz
            ,
        };

        var result = JsonConvert.SerializeObject(errorResponse);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(result);
    }
}
