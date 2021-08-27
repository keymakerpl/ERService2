using ERService.DataAccess.EntityFramework.Entities;

namespace ERService.DataAccess.EntityFramework.Abstractions
{
    public interface ICustomerRepository : IGenericRepository<int, Customer>
    {
    }
}