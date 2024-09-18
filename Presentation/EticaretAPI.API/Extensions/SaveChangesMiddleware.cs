using EticaretAPI.Persistence.Contexts;

namespace EticaretAPI.API.Extensions;

public class SaveChangesMiddleware
	{
	private readonly RequestDelegate _next;

	public SaveChangesMiddleware(RequestDelegate next)
		{
		_next = next;
		}

	public async Task InvokeAsync(HttpContext context , EticaretAPIDbContext dbContext)
		{
		// HTTP isteği işleniyor
		await _next(context);

		// Eğer değişiklik varsa SaveChangesAsync tetikleniyor
		if(dbContext.ChangeTracker.HasChanges())
			{
			await dbContext.SaveChangesAsync();
			}
		}
	}