using System.Net;
using EticaretAPI.Application.Exceptions;
using Newtonsoft.Json;

public class ExceptionHandlingMiddleware
{
	private readonly RequestDelegate _next;

	public ExceptionHandlingMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext httpContext)
	{
		try
		{
			await _next(httpContext);
		}
		catch (ApiException ex)
		{
			await HandleExceptionAsync(httpContext, ex);
		}
		catch (Exception ex)
		{
			await HandleExceptionAsync(httpContext, ex);
		}
	}

	private Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		context.Response.ContentType = "application/json";
		context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

		var response = new
		{
			message = exception is ApiException ? exception.Message : "Internal Server Error",
		};

		return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
	}
}
