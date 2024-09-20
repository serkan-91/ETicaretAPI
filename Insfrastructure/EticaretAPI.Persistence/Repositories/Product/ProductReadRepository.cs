namespace EticaretAPI.Persistence.Repositories;

public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
{
    public ProductReadRepository(EticaretAPIDbContext context)
        : base(context) { }
}
