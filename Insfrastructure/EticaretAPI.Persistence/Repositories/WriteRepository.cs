namespace EticaretAPI.Persistence.Repositories;

public class WriteRepository<T>(EticaretAPIDbContext _context) : IWriteRepository<T>
	where T : BaseEntity
	{
	public DbSet<T> Table => _context.Set<T>();

	public async Task AddAsync(T model) => await Table.AddAsync(model);

	public async Task AddRangeAsync(List<T> datas) => await Table.AddRangeAsync(datas);

	public void RemoveAsync(T model) => Table.Remove(model);

	public void RemoveRangeAsync(List<T> datas) => Table.RemoveRange(datas);

	public async Task RemoveAsync(string id) {
		var entity = await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id))
		?? throw new Exception("Entity not found");
		Table.Remove(entity);
		}


	public void UpdateAsync(T model) => Table.Update(model);
	}