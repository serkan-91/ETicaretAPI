namespace EticaretAPI.Persistence.Repositories;

public class FileReadRepository : ReadRepository<Domain.Entities.File>, IFileReadRepository
{
	public FileReadRepository(EticaretAPIDbContext _context)
		: base(_context) { }
}
