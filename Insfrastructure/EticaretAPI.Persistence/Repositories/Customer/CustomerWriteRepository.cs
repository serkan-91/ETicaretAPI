namespace EticaretAPI.Persistence.Repositories;

public class CustomerWriteRepository : WriteRepository<Customer>, ICustomerWriteRepository
{
	public CustomerWriteRepository(EticaretAPIDbContext context)
		: base(context) { }
}
