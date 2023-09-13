using EticaretAPI.Application.Repositories;
using EticaretAPI.Persistence.Contexts;

using F = EticaretAPI.Domain.Entities;
namespace EticaretAPI.Persistence.Repositories 
{
    public class FileWriteRepository : WriteRepository<F::File>, IFileWriteRepository
    {
        public FileWriteRepository(EticaretAPIDbContext context) : base(context)
        {
        }
 
    }
}
