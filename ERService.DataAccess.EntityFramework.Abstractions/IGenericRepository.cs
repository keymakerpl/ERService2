using ERService.FunctionalCSharp;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ERService.DataAccess.EntityFramework.Abstractions
{
    public interface IGenericRepository<TKey, TEntity> : IRepository
        where TEntity : class
    {
        TEntity GetById(TKey id);
        Task<Maybe<TEntity>> GetByIdAsync(TKey id);
        Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate, int skip, int take);
        Task<IEnumerable<TEntity>> GetAllAsync();
        List<T> Get<T>(string sqlQuery, object[] parameters);
        Task<List<T>> GetAsync<T>(string sqlQuery, object[] parameters);
        Task<IEnumerable<TEntity>> FindByIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProp);

        Task<bool> SaveAsync();

        bool HasChanges();
        Task<bool> Add(TEntity model);
        Task<bool> Update(TEntity model);
        Task<bool> Remove(TEntity model);
        Task<Result> RemoveById(TKey key);
        void ReloadEntity(TEntity entity);
        Task ReloadEntityAsync(TEntity entity);
    }
}