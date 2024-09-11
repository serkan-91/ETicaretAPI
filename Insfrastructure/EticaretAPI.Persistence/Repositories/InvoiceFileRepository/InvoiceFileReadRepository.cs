namespace EticaretAPI.Persistence.Repositories;

public class InvoiceFileReadRepository
	: ReadRepository<Domain.Entities.InvoiceImageFile>,
		IInvoiceFileReadRepository
{
	public InvoiceFileReadRepository(EticaretAPIDbContext _context)
		: base(_context) { }
}
