using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Etdb.ServiceBase.Domain.Abstractions.Entities;
using Etdb.ServiceBase.EntityRepository.Abstractions.Context;
using Etdb.ServiceBase.EntityRepository.Abstractions.Generics;
using Microsoft.EntityFrameworkCore;

namespace Etdb.ServiceBase.EntityRepository.Generics
{
    public abstract class EntityRepository<TEntity, TId> : IEntityRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>, new()
        where TId : IEquatable<TId>
    {
        private readonly EntityDbContext context;

        protected EntityRepository(EntityDbContext context)
        {
            this.context = context;
        }
        
        public virtual IEnumerable<TEntity> GetAll()
        {
            return this.CreateQuery().ToArray();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this.CreateQuery()
                .ToArrayAsync()
                .ConfigureAwait(false);
        }

        public virtual IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            return this.BuildIncludes(includes).ToArray();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            return await this.BuildIncludes(includes).ToArrayAsync();
        }

        public virtual IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return this.CreateQuery()
                .Where(predicate)
                .ToArray();
        }

        public virtual async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.CreateQuery()
                .Where(predicate)
                .ToArrayAsync()
                .ConfigureAwait(false);
        }

        public virtual IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return this.BuildIncludes(includes)
                .Where(predicate)
                .ToArray();
        }

        public virtual async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, 
            params Expression<Func<TEntity, object>>[] includes)
        {
            return await this.BuildIncludes(includes)
                .Where(predicate)
                .ToArrayAsync()
                .ConfigureAwait(false);
        }

        public virtual TEntity Find(TId key)
        {
            return this.CreateQuery()
                .FirstOrDefault(entity => entity.Id.Equals(key));
        }

        public virtual async Task<TEntity> FindAsync(TId key)
        {
            return await this.CreateQuery()
                .FirstOrDefaultAsync(entity => entity.Id.Equals(key))
                .ConfigureAwait(false);
        }

        public virtual TEntity Find(TId key, params Expression<Func<TEntity, object>>[] includes)
        {
            return this.BuildIncludes(includes)
                .FirstOrDefault(entity => entity.Id.Equals(key));
        }

        public virtual async Task<TEntity> FindAsync(TId key, params Expression<Func<TEntity, object>>[] includes)
        {
            return await this.BuildIncludes(includes)
                .FirstOrDefaultAsync(entity => entity.Id.Equals(key))
                .ConfigureAwait(false);
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return this.CreateQuery().FirstOrDefault(predicate);
        }

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.CreateQuery()
                .FirstOrDefaultAsync(predicate)
                .ConfigureAwait(false);
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return this.BuildIncludes(includes)
                .FirstOrDefault(predicate);
        }

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return await this.BuildIncludes(includes)
                .FirstOrDefaultAsync(predicate)
                .ConfigureAwait(false);
        }

        public virtual void Add(TEntity entity)
        {
            this.context.Set<TEntity>().Add(entity);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await this.context.Set<TEntity>().AddAsync(entity);
        }

        public virtual void Edit(TEntity entity)
        {
            var entry = this.context.Entry(entity);

            entry.State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            var entry = this.context.Entry(entity);

            entry.State = EntityState.Deleted;
        }

        public virtual bool EnsureChanges()
        {
            var modifiedCount = this.context.SaveChanges();

            return modifiedCount > 0;
        }

        public virtual async Task<bool> EnsureChangesAsync()
        {
            var modifiedCount = await this.context.SaveChangesAsync();

            return modifiedCount > 0;
        }

        public virtual IQueryable<TEntity> GetQueryAble(bool noTracking = false)
        {
            return noTracking
                ? this.CreateQuery().AsNoTracking()
                : this.CreateQuery();
        }

        public virtual int Count()
        {
            return this.CreateQuery()
                .Select(entity => entity.Id)
                .Count();
        }

        public virtual async Task<int> CountAsync()
        {
            return await this.CreateQuery()
                .Select(entity => entity.Id)
                .CountAsync()
                .ConfigureAwait(false);
        }

        private IQueryable<TEntity> BuildIncludes(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = this.CreateQuery();

            return includes.Aggregate(query, (current, include) => current.Include(include));
        }

        private IQueryable<TEntity> CreateQuery()
        {
            return this.context.Set<TEntity>().AsQueryable();
        }
    }
}