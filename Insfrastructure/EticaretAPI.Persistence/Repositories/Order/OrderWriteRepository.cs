namespace EticaretAPI.Persistence.Repositories;

public class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository
{
    public OrderWriteRepository(EticaretAPIDbContext context)
        : base(context) { }
}
