using ERService.DataAccess.EntityFramework.Abstractions;
using ERService.DataAccess.EntityFramework.Entities;

namespace ERService.DataAccess.EntityFramework.SqlServer.Repositories
{
    public class OrderRepository : GenericRepository<int, Order, ERServiceDbContext>, IOrderRepository
    {
        public OrderRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}
