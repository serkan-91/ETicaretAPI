namespace EticaretAPI.Persistence.Repositories;

public class CustomerReadRepository : ReadRepository<Customer>, ICustomerReadRepository
{
	public CustomerReadRepository(EticaretAPIDbContext context)
		: base(context) { }
}
