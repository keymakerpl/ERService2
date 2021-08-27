using Microsoft.EntityFrameworkCore;

namespace ERService.DataAccess.EntityFramework.Abstractions
{
    public interface IContextFactory
    {
        T CreateContext<T>() where T : DbContext;
    }
}