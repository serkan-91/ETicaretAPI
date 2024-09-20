namespace EticaretAPI.Persistence.Contexts;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EticaretAPIDbContext>
{
    public EticaretAPIDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<EticaretAPIDbContext> dbContextOptionsBuilder = new();
        dbContextOptionsBuilder.UseNpgsql(Configurations.ConnectionString);
        return new EticaretAPIDbContext(dbContextOptionsBuilder.Options);
    }
}
