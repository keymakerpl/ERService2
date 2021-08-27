using ERService.DataAccess.EntityFramework.Abstractions;
using Prism.Ioc;

namespace ERService.DataAccess.EntityFramework.SqlServer.Repository
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IContainerProvider containerProvider;

        public RepositoryFactory(IContainerProvider containerProvider) => 
            this.containerProvider = containerProvider;

        public T GetRepository<T>() where T : IRepository => 
            containerProvider.Resolve<T>();
    }
}
