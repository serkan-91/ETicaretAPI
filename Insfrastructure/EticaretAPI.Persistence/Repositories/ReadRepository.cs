using EticaretAPI.Application.RequestParameters;

namespace EticaretAPI.Persistence.Repositories;

public class ReadRepository<T>(EticaretAPIDbContext _context) : IReadRepository<T>
	where T : BaseEntity
	{
	//private readonly EticaretAPIDbContext _context = context;
	public DbSet<T> Table => _context.Set<T>();

	public async Task<List<T>> GetAllAsync(bool tracking = true) =>
		await (tracking ? Table.ToListAsync() : Table.AsNoTracking().ToListAsync());

	public async Task<List<T>> GetWhere(Expression<Func<T , bool>> method , bool tracking = true) =>
		await (
			tracking
				? Table.Where(method).ToListAsync()
				: Table.AsNoTracking().Where(method).ToListAsync()
		);

	public async Task<T> GetSingleAsync(Expression<Func<T , bool>> method , bool tracking = true) =>
		await (
			tracking
				? Table.FirstOrDefaultAsync(method)
				: Table.AsNoTracking().FirstOrDefaultAsync(method)
		) ?? default!;

	public async Task<T> GetByIdAsync(string id , bool tracking = true) =>
		await (
			tracking
				? Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id))
				: Table.AsNoTracking().FirstOrDefaultAsync(data => data.Id == Guid.Parse(id))
		) ?? default!;

	public async Task<T> FindAsync(string id) =>
		await (Table.FindAsync(Guid.Parse(id))) ?? default!;

	public async Task<T> GetWithIncludeAsync(
		Expression<Func<T , bool>> filter ,
		bool tracking = true ,
		params Expression<Func<T , object>>[] includes
	) {
		var query = !tracking ? Table.AsNoTracking() : Table;
		query = includes.Aggregate(query , (current , include) => current.Include(include));
		return await query.FirstOrDefaultAsync(filter) ?? default!;
		}

	public async Task<PagingResult<T>> GetPagedAsync(Paginations pagination , bool tracking = true) {
		var query = !tracking ? Table.AsNoTracking().AsQueryable() : Table.AsQueryable();
		var totalCount = await query.CountAsync();

		var items = await query
			.Skip((pagination.Page - 1) * pagination.Size)
			.Take(pagination.Size)
			.ToListAsync();

		return new PagingResult<T>
			{
			Items = items ,
			TotalCount = totalCount
			};
		}
	}