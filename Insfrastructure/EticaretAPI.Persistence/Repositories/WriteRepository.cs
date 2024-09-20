namespace EticaretAPI.Persistence.Repositories;

public class WriteRepository<T>(EticaretAPIDbContext _context) : IWriteRepository<T>
    where T : BaseEntity
{
    public DbSet<T> Table => _context.Set<T>();

    public Task AddAsync(T model, CancellationToken cancellation) =>
        Task.Run(() => Table.AddAsync(model, cancellation).ConfigureAwait(false));

    public Task AddRangeAsync(List<T> datas, CancellationToken cancellationToken) =>
        Task.Run(() => Table.AddRangeAsync(datas, cancellationToken).ConfigureAwait(false));

    public Task RemoveAsync(T model, CancellationToken cancellationToken) =>
        Task.Run(() => Table.Remove(model), cancellationToken);

    public void RemoveRangeAsync(List<T> datas) => Table.RemoveRange(datas);

    public async Task RemoveAsync(string id, CancellationToken cancellationToken)
    {
        var entity =
            await Table
                .FirstOrDefaultAsync(data => data.Id == Guid.Parse(id), cancellationToken)
                .ConfigureAwait(false) ?? throw new Exception("Entity not found");
        Table.Remove(entity);
    }

    public void UpdateAsync(T model) => Table.Update(model);
}
