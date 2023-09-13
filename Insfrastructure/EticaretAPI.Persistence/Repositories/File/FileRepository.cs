using EticaretAPI.Application.Repositories;
using EticaretAPI.Persistence.Contexts;

using F = EticaretAPI.Domain.Entities;

namespace EticaretAPI.Persistence.Repositories
{
    public class FileReadRepository : ReadRepository<F::File>, IFileReadRepository
    {
        public FileReadRepository(EticaretAPIDbContext context) : base(context)
        {
            
        }

       
    }
}