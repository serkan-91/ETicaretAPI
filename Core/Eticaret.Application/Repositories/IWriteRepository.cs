using System.Threading;

namespace EticaretAPI.Application.Repositories;

public interface IWriteRepository<T> : IRepository<T>
    where T : BaseEntity
{
    Task AddAsync(T model, CancellationToken cancellationToken);

    Task AddRangeAsync(List<T> model, CancellationToken cancellationToken);

    Task RemoveAsync(string id, CancellationToken cancellationToken);

    Task RemoveAsync(T model, CancellationToken cancellationToken);

    void RemoveRangeAsync(List<T> datas);

    void UpdateAsync(T model);
}
