using EticaretAPI.Persistence.Contexts;

namespace EticaretAPI.API.Extensions;

public class SaveChangesMiddleware
{
    private readonly RequestDelegate _next;

    public SaveChangesMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, EticaretAPIDbContext dbContext)
    {
        // CancellationToken'ı HttpContext üzerinden alıyoruz
        var cancellationToken = context.RequestAborted;

        // HTTP isteği işleniyor
        await _next(context).ConfigureAwait(false);

        // Eğer değişiklik varsa SaveChangesAsync tetikleniyor
        if (dbContext.ChangeTracker.HasChanges())
        {
            try
            {
                await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                Console.Write("veritabanina yazildi.");
            }
            catch (Exception ex)
            {
                // Hataları loglayın ya da handle edin
                Console.WriteLine($"Error during SaveChanges: {ex.Message}");
                throw;
            }
        }
    }
}
