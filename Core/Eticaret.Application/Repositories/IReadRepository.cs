using System.Linq.Expressions;
using EticaretAPI.Application.RequestParameters;

namespace EticaretAPI.Application.Repositories;

public interface IReadRepository<T> : IRepository<T>
    where T : BaseEntity
{
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken, bool tracking = true);

    Task<List<T>> GetWhereAsync(
        Expression<Func<T, bool>> method,
        CancellationToken cancellationToken,
        bool tracking = true
    );

    Task<T> GetSingleAsync(
        Expression<Func<T, bool>> method,
        CancellationToken cancellationToken,
        bool tracking = true
    );

    Task<T> GetByIdAsync(CancellationToken cancellationToken, string id, bool tracking = true);

    Task<T> FindAsync(CancellationToken cancellationToken, string id);

    Task<T> GetWithIncludeAsync(
        Expression<Func<T, bool>> filter,
        CancellationToken cancellationToken,
        bool tracking = true,
        params Expression<Func<T, object>>[] includes
    );

    Task<PagingResult<T>> GetPagedAsync(
        CancellationToken cancellationToken,
        Pagination pagination,
        bool tracking = true
    );
}
