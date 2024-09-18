using EticaretAPI.Application.RequestParameters;
using System.Linq.Expressions;

namespace EticaretAPI.Application.Repositories;

public interface IReadRepository<T> : IRepository<T>
	where T : BaseEntity
	{
	Task<List<T>> GetAllAsync(bool tracking = true);

	Task<List<T>> GetWhere(Expression<Func<T , bool>> method , bool tracking = true);

	Task<T> GetSingleAsync(Expression<Func<T , bool>> method , bool tracking = true);

	Task<T> GetByIdAsync(string id , bool tracking = true);

	Task<T> FindAsync(string id);

	Task<T> GetWithIncludeAsync(Expression<Func<T , bool>> filter , bool tracking = true , params Expression<Func<T , object>>[] includes);

	Task<PagingResult<T>> GetPagedAsync(Paginations pagination , bool tracking = true);
	}