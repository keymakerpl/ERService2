using ERService.DataAccess.EntityFramework.Abstractions;
using Microsoft.EntityFrameworkCore;
using Prism.Ioc;

namespace ERService.DataAccess.EntityFramework.SqlServer
{
    public class DbContextFactory : IContextFactory
    {
        private readonly IContainerProvider containerProvider;

        public DbContextFactory(IContainerProvider containerProvider) => this.containerProvider = containerProvider;

        public T CreateContext<T>() where T : DbContext => containerProvider.Resolve<T>();
    }
}
