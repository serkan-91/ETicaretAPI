namespace EticaretAPI.Persistence.Repositories;

public class OrderReadRepository : ReadRepository<Order>, IOrderReadRepository
{
    public OrderReadRepository(EticaretAPIDbContext context)
        : base(context) { }
}
