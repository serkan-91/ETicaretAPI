namespace EticaretAPI.Persistence.Repositories;

public class ReadRepository<T>(EticaretAPIDbContext _context) : IReadRepository<T>
	where T : BaseEntity
{
	//private readonly EticaretAPIDbContext _context = context;
	public DbSet<T> Table => _context.Set<T>();

	public IQueryable<T> GetAll(bool tracking = true) =>
		tracking ? Table.AsQueryable() : Table.AsQueryable().AsNoTracking();

	public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true) =>
		tracking ? Table.Where(method) : Table.Where(method).AsNoTracking();

	public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true) =>
		await (
			tracking
				? Table.FirstOrDefaultAsync(method)
				: Table.AsNoTracking().FirstOrDefaultAsync(method)
		) ?? default!;

	public async Task<T> GetByIdAsync(string id, bool tracking = true) =>
		await (
			tracking
				? Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id))
				: Table.AsNoTracking().FirstOrDefaultAsync(data => data.Id == Guid.Parse(id))
		) ?? default!;

	public async Task<T> FindAsync(string id) =>
		await (Table.FindAsync(Guid.Parse(id))) ?? default!;
}
