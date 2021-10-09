using ERService.DataAccess.EntityFramework.Abstractions;
using ERService.DataAccess.EntityFramework.Entities;
using ERService.FunctionalCSharp;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERService.DataAccess.EntityFramework.SqlServer.Repositories
{
    public class CustomerRepository : GenericRepository<int, Customer, ERServiceDbContext>, ICustomerRepository
    {
        public CustomerRepository(IContextFactory contextFactory) : base(contextFactory)
        {

        }

        public override async Task<Maybe<Customer>> GetByIdAsync(int id) =>
            await Disposable.Of(() => contextFactory.CreateContext<ERServiceDbContext>())
                            .Use(async context => await context.Set<Customer>()
                                                               .Include(a => a.CustomerAddresses)
                                                               .SingleOrDefaultAsync(c => c.Id == id));

        public override async Task<IEnumerable<Customer>> GetAllAsync() =>
            await Disposable.Of(() => contextFactory.CreateContext<ERServiceDbContext>())
                            .Use(async context => await context.Set<Customer>()
                                                               .Include(a => a.CustomerAddresses)
                                                               .ToListAsync());
    }
}
