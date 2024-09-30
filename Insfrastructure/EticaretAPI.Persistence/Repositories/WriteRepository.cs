namespace EticaretAPI.Persistence.Repositories;

public class WriteRepository<T>(EticaretAPIDbContext _context) : IWriteRepository<T>
	where T : BaseEntity
{
	public DbSet<T> Table => _context.Set<T>();

	public async Task AddAsync(T model) => await Table.AddAsync(model);

	public Task AddRangeAsync(List<T> datas , CancellationToken cancellationToken) =>
		Task.Run(() => Table.AddRangeAsync(datas , cancellationToken).ConfigureAwait(false));

	public Task RemoveAsync(T model , CancellationToken cancellationToken) =>
		Task.Run(() => Table.Remove(model) , cancellationToken);

	public void RemoveRangeAsync(List<T> datas) => Table.RemoveRange(datas);

	public async Task RemoveAsync(string id , CancellationToken cancellationToken)
	{
		var entity =
			await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id) , cancellationToken)
			?? throw new Exception("Entity not found");
		Table.Remove(entity);
	}

	public async Task Update(T model)
	{
		var existingEntity = await Table.FindAsync(model.Id);
		ArgumentNullException.ThrowIfNull(existingEntity , nameof(existingEntity));

		_context.Entry(existingEntity).CurrentValues.SetValues(model);
		Table.Update(existingEntity);
	}

	public async Task SaveChangesAsync() => await _context.SaveChangesAsync();



	public Task UpdateAsync(T model)
	{
		throw new NotImplementedException();
	}
}
