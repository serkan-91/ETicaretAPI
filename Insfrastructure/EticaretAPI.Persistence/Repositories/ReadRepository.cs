using EticaretAPI.Application.RequestParameters;

namespace EticaretAPI.Persistence.Repositories;

public class ReadRepository<T>(EticaretAPIDbContext _context) : IReadRepository<T>
    where T : BaseEntity
{
    //private readonly EticaretAPIDbContext _context = context;
    public DbSet<T> Table => _context.Set<T>();

    public Task<List<T>> GetAllAsync(CancellationToken cancellationToken, bool tracking = true) =>
        Task.Run(
            () =>
                tracking
                    ? Table.ToListAsync(cancellationToken)
                    : Table.AsNoTracking().ToListAsync(cancellationToken)
        );

    public Task<List<T>> GetWhereAsync(
        Expression<Func<T, bool>> method,
        CancellationToken cancellationToken,
        bool tracking = true
    ) =>
        Task.Run(
            () =>
                tracking
                    ? Table.Where(method).ToListAsync(cancellationToken)
                    : Table.AsNoTracking().Where(method).ToListAsync(cancellationToken)
        );

    public async Task<T> GetSingleAsync(
        Expression<Func<T, bool>> method,
        CancellationToken cancellationToken,
        bool tracking = true
    ) =>
        await (
            tracking
                ? Table.FirstOrDefaultAsync(method, cancellationToken)
                : Table.AsNoTracking().FirstOrDefaultAsync(method, cancellationToken)
        ) ?? default!;

    public async Task<T> GetByIdAsync(
        CancellationToken cancellationToken,
        string id,
        bool tracking = true
    ) =>
        await (
            tracking
                ? Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id), cancellationToken)
                : Table
                    .AsNoTracking()
                    .FirstOrDefaultAsync(data => data.Id == Guid.Parse(id), cancellationToken)
        ) ?? default!;

    public async Task<T> FindAsync(CancellationToken cancellationToken, string id) =>
        await (Table.FindAsync(Guid.Parse(id), cancellationToken)) ?? default!;

    public async Task<T> GetWithIncludeAsync(
        Expression<Func<T, bool>> filter,
        CancellationToken cancellationToken,
        bool tracking = true,
        params Expression<Func<T, object>>[] includes
    )
    {
        var query = !tracking ? Table.AsNoTracking() : Table;
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.FirstOrDefaultAsync(filter, cancellationToken).ConfigureAwait(false)
            ?? default!;
    }

    public async Task<PagingResult<T>> GetPagedAsync(
        CancellationToken cancellationToken,
        Paginations pagination,
        bool tracking = true
    )
    {
        var query = !tracking ? Table.AsNoTracking().AsQueryable() : Table.AsQueryable();
        var totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

        var items = await query
            .Skip((pagination.Page) * pagination.Size)
            .Take(pagination.Size)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        return new PagingResult<T> { Items = items, TotalCount = totalCount };
    }
}
