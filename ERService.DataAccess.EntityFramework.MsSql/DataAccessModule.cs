using ERService.DataAccess.EntityFramework.Abstractions;
using ERService.DataAccess.EntityFramework.SqlServer.Repositories;
using ERService.DataAccess.EntityFramework.SqlServer.Repository;
using Prism.Ioc;
using Prism.Modularity;

namespace ERService.DataAccess.EntityFramework.SqlServer
{
    public class DataAccessModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }
        public void RegisterTypes(IContainerRegistry containerRegistry) =>
            containerRegistry.Register<ERServiceDbContext>(() => new ERServiceDbContext())
                             .Register<IRepositoryFactory, RepositoryFactory>()
                             .Register<IContextFactory, DbContextFactory>()
                             .Register<ICustomerRepository, CustomerRepository>();
    }
}