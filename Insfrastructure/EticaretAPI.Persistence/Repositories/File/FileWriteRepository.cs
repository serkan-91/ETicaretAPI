namespace EticaretAPI.Persistence.Repositories;

public class FileWriteRepository : WriteRepository<Domain.Entities.File>, IFileWriteRepository
{
    public FileWriteRepository(EticaretAPIDbContext _context)
        : base(_context) { }
}
