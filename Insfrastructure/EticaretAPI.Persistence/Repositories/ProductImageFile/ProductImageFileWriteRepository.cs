namespace EticaretAPI.Persistence.Repositories;

public class ProductImageFileWriteRepository
	: WriteRepository<Domain.Entities.ProductImageFile>,
		IProductImageFileWriteRepository
	{
	public ProductImageFileWriteRepository(EticaretAPIDbContext _context)
		: base(_context) { }
	}