using Dapper;
using ERService.Contracts.Data;
using ERService.DataAccess.EntityFramework.Abstractions;
using ERService.FunctionalCSharp;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ERService.DataAccess.EntityFramework.SqlServer
{
    public class GenericRepository<TKey, TEntity, TContext> : IGenericRepository<TKey, TEntity>
        where TContext : DbContext
        where TEntity : class
    {
        protected readonly IContextFactory contextFactory;

        protected GenericRepository(IContextFactory contextFactory) 
        {
            Disposable.Of(() => contextFactory.CreateContext<TContext>()).Use(context => context.Database.Migrate());
            this.contextFactory = contextFactory;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() =>
            await Disposable.Of(() => contextFactory.CreateContext<TContext>())
                            .Use(async context => await context.Set<TEntity>().ToListAsync());

        public virtual TEntity GetById(TKey id) =>
            Disposable.Of(() => contextFactory.CreateContext<TContext>())
                      .Use(context => context.Set<TEntity>().Find(id));

        public virtual async Task<Maybe<TEntity>> GetByIdAsync(TKey id) =>
            await Disposable.Of(() => contextFactory.CreateContext<TContext>())
                            .Use(async context => await context.Set<TEntity>().FindAsync(id));

        public virtual async Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate) =>
            await Disposable.Of(() => contextFactory.CreateContext<TContext>())
                            .Use(async context => await context.Set<TEntity>().Where(predicate).ToListAsync());

        public virtual async Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate, int skip, int take) =>
            await Disposable.Of(() => contextFactory.CreateContext<TContext>())
                            .Use(async context => await context.Set<TEntity>().Where(predicate).Skip(skip).Take(take).ToListAsync());

        public virtual async Task<IEnumerable<TEntity>> FindByIncludeAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProps) {
            var query = GetAllIncluding(includeProps);
            return await query.AsExpandable().Where(predicate).ToListAsync();
        }

        private IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProps) => 
            Disposable.Of(() => contextFactory.CreateContext<TContext>())
                      .Use(context => 
                      { 
                          IQueryable<TEntity> queryable = context.Set<TEntity>(); 
                          return includeProps.Aggregate(queryable, (current, includeProperty) => current.Include(includeProperty)); 
                      });

        public virtual async Task<List<T>> GetAsync<T>(string sqlQuery, object[] parameters) => 
            await Disposable.Of(() => contextFactory.CreateContext<TContext>())
                            .Use(async context => 
                            { 
                                await using var connection = context.Database.GetDbConnection(); 
                                await context.Database.OpenConnectionAsync(); 
                                return (await connection.QueryAsync<T>(sqlQuery, parameters)).ToList(); 
                            });

        public virtual List<T> Get<T>(string sqlQuery, object[] parameters) => 
            Disposable.Of(() => contextFactory.CreateContext<TContext>())
                      .Use(context =>
                      {
                          using var connection = context.Database.GetDbConnection();
                          context.Database.OpenConnection();
                          return connection.Query<T>(sqlQuery, parameters).ToList();
                      });

        public virtual async Task<bool> SaveAsync() => 
            await Disposable.Of(() => contextFactory.CreateContext<TContext>())
                            .Use(async context => await SaveAsyncInternal(context));

        private async Task<bool> SaveAsyncInternal(TContext context)
        {
            var addedEntities = context.ChangeTracker.Entries()
                                                     .Where(x => x.State == EntityState.Added)
                                                     .Select(x => x.Entity)
                                                     .ToList();

            foreach (var entry in addedEntities)
            {
                var added = entry as IAuditableEntity;
                if (added != null && added.CreatedOn == DateTime.MinValue)
                    added.CreatedOn = DateTime.Now;
            }

            var modifiedEntities = context.ChangeTracker.Entries()
                                                        .Where(x => x.State == EntityState.Modified)
                                                        .Select(x => x.Entity)
                                                        .ToList();

            foreach (var entry in modifiedEntities)
            {
                var modified = entry as IAuditableEntity;
                if (modified != null)
                    modified.LastModifiedOn = DateTime.Now;

                var versioned = entry as IVersionedEntity;
                if (versioned != null)
                    versioned.RowVersion++;
            }

            var deletedEntities = context.ChangeTracker.Entries()
                                                       .Where(x => x.State == EntityState.Deleted)
                                                       .Select(x => x.Entity)
                                                       .ToList();

            foreach (var entry in deletedEntities)
            {
                var deleted = entry as ISoftDeletable;
                if (deleted != null)
                {
                    context.Entry(deleted).State = EntityState.Modified;
                    deleted.IsDeleted = true;
                }
            }

            var savedElements = await context.SaveChangesAsync();
            return savedElements > 0;
        }

        public virtual bool HasChanges() => Disposable.Of(() => contextFactory.CreateContext<TContext>())
                                                      .Use(context => context.ChangeTracker.HasChanges());

        public virtual async Task<bool> Add(TEntity model) => 
            await Disposable.Of(() => contextFactory.CreateContext<TContext>())
                            .Use(async context => 
                            { 
                                context.Set<TEntity>().Add(model); 
                                return await SaveAsyncInternal(context); 
                            });

        public virtual async Task<bool> Update(TEntity model) => 
            await Disposable.Of(() => contextFactory.CreateContext<TContext>())
                            .Use(async context => 
                            { 
                                context.Set<TEntity>().Update(model); 
                                return await SaveAsyncInternal(context); 
                            });

        public virtual async Task<bool> Remove(TEntity model) => 
            await Disposable.Of(() => contextFactory.CreateContext<TContext>())
                            .Use(async context => 
                            { 
                                context.Set<TEntity>().Remove(model); 
                                return await SaveAsyncInternal(context); 
                            });

        public virtual async Task<Result> RemoveById(TKey key) => 
            await Disposable.Of(() => contextFactory.CreateContext<TContext>())
                            .Use(async context =>
                            {
                                return await Maybe.From(await context.FindAsync<TEntity>(key)
                                                                     .ConfigureAwait(false))
                                     .ToResult("Entity not found!")
                                     .Bind(entity => Result.Try(async () => await Remove(entity).ConfigureAwait(false)))
                                     .OnFailure(error => Debug.WriteLine(error));
                            });

        public virtual void ReloadEntity(TEntity entity) =>
            Disposable.Of(() => contextFactory.CreateContext<TContext>())
                      .Use(context => context.Entry(entity).Reload());

        public virtual async Task ReloadEntityAsync(TEntity entity) => 
            await Disposable.Of(() => contextFactory.CreateContext<TContext>())
                            .Use(async context => await context.Entry(entity).ReloadAsync());
    }
}