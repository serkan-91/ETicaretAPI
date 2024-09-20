using Microsoft.EntityFrameworkCore;

namespace EticaretAPI.Application.Repositories;

public interface IRepository<T>
    where T : BaseEntity
{
    DbSet<T> Table { get; }
}
