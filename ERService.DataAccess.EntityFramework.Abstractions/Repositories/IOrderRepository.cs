using ERService.DataAccess.EntityFramework.Entities;

namespace ERService.DataAccess.EntityFramework.Abstractions
{

    public interface IOrderRepository : IGenericRepository<int, Order>
    {
    }
}