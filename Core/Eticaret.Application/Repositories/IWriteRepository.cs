using System.Threading;

namespace EticaretAPI.Application.Repositories;

public interface IWriteRepository<T> : IRepository<T>
    where T : BaseEntity
{
    Task AddAsync(T model);

    Task AddRangeAsync(List<T> model, CancellationToken cancellationToken);

    Task RemoveAsync(string id, CancellationToken cancellationToken);

    Task RemoveAsync(T model, CancellationToken cancellationToken);

    void RemoveRangeAsync(List<T> datas);

    Task UpdateAsync(T model);
    Task SaveChangesAsync();
}
