namespace EticaretAPI.Persistence.Repositories;

public class WriteRepository<T>(EticaretAPIDbContext _context) : IWriteRepository<T> where T : BaseEntity
	{
	public DbSet<T> Table => _context.Set<T>();

	public async Task<bool> AddAsync(T model) => (await Table.AddAsync(model)).State == EntityState.Added;

	public Task<bool> AddRangeAsync(List<T> model) => Task.FromResult(Table.AddRangeAsync(model).IsCompletedSuccessfully);

	public bool Remove(T model) => Table.Remove(model).State == EntityState.Deleted;

	public bool RemoveRange(List<T> datas)
		{ Table.RemoveRange(datas); return true; }

	public async Task<bool> RemoveAsync(string id) => (await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id))) is T model && Remove(model);

	public bool Update(T model) => Table.Update(model).State == EntityState.Modified;

	public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
	}