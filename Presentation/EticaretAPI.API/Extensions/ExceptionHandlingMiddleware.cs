using System.Net.Sockets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace EticaretAPI.API.Extensions;

public class ExceptionHandlingMiddleware(
	RequestDelegate _next ,
	ILogger<ExceptionHandlingMiddleware> Logger
)
	{
	public async Task Invoke(HttpContext context)
		{
		try
			{
			// Sonraki middleware veya endpoint'e geçiş yap
			await _next(context);
			}
		catch(Exception ex)
			{
			// İstisnayı logla
			Logger.LogError(ex , "an exception was occur");

			// İstisnayı işle ve uygun yanıtı dön
			await HandleExceptionAsync(context , ex);
			}
		}

	private Task HandleExceptionAsync(HttpContext context , Exception exception)
		{
		// HTTP durum kodunu ve mesajını belirle
		int statusCode;
		string message;

		switch(exception)
			{
			case ArgumentNullException argNullEx:
				statusCode = StatusCodes.Status400BadRequest;
				message = argNullEx.Message;
				break;

			case UnauthorizedAccessException unauthorizedEx:
				statusCode = StatusCodes.Status401Unauthorized;
				message = $"Access Denied. : {unauthorizedEx.Message}";
				break;

			case InvalidOperationException invalidOpEx:
				statusCode = StatusCodes.Status400BadRequest;
				message = invalidOpEx.Message;
				break;

			case FormatException formatEx:
				statusCode = StatusCodes.Status400BadRequest;
				message = $"Invalid Data Format  {formatEx.Message}";
				break;

			case NpgsqlException npgsqlEx:
				statusCode = StatusCodes.Status500InternalServerError;
				message = "Connection Error : " + npgsqlEx.Message;
				break;

			case SocketException socketEx:
				statusCode = StatusCodes.Status500InternalServerError;
				message = "Network Error : " + socketEx.Message;
				break;

			default:
				statusCode = StatusCodes.Status500InternalServerError;
				message = "Unexpected Error.";
				break;
			}

		context.Response.ContentType = "application/json";
		context.Response.StatusCode = statusCode;

		// Yanıt olarak dönecek nesne
		var response = new { error = message };

		// Yanıtı JSON formatında gönder
		return context.Response.WriteAsJsonAsync(response);
		}
	}