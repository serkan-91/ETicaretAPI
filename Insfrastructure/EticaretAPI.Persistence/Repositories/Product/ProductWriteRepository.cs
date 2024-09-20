namespace EticaretAPI.Persistence.Repositories;

public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
{
    public ProductWriteRepository(EticaretAPIDbContext context)
        : base(context) { }
}
