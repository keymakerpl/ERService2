namespace ERService.DataAccess.EntityFramework.Abstractions
{
    public interface IRepositoryFactory
    {
        T GetRepository<T>() where T : IRepository;
    }

    public interface IRepository { }
}