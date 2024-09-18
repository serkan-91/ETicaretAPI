namespace EticaretAPI.Application.Repositories;

public interface IWriteRepository<T> : IRepository<T>
	where T : BaseEntity
	{
	Task AddAsync(T model);

	Task AddRangeAsync(List<T> model);

	void RemoveAsync(T model);

	Task RemoveAsync(string id);

	void RemoveRangeAsync(List<T> datas);

	void UpdateAsync(T model);
	}