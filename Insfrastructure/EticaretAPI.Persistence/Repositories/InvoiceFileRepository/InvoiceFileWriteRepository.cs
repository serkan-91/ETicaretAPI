namespace EticaretAPI.Persistence.Repositories;

public class InvoiceFileWriteRepository
	: WriteRepository<Domain.Entities.InvoiceImageFile>,
		IInvoiceFileWriteRepository
{
	public InvoiceFileWriteRepository(EticaretAPIDbContext _context)
		: base(_context) { }
}
