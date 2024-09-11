namespace EticaretAPI.Persistence.Repositories;

public class ProductImageFileReadRepository
	: ReadRepository<Domain.Entities.ProductImageFile>,
		IProductImageFileReadRepository
{
	public ProductImageFileReadRepository(EticaretAPIDbContext _context)
		: base(_context) { }
}
