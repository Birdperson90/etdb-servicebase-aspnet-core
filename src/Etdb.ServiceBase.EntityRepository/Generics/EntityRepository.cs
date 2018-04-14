using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Etdb.ServiceBase.EntityDomain.Abstractions;
using Etdb.ServiceBase.EntityRepository.Abstractions.Context;
using Etdb.ServiceBase.EntityRepository.Abstractions.Generics;
using Microsoft.EntityFrameworkCore;

namespace Etdb.ServiceBase.EntityRepository.Generics
{
    public abstract class EntityRepository<TEntity, TKey> : IEntityRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        private readonly EntityDbContext context;

        protected EntityRepository(EntityDbContext context)
        {
            this.context = context;
        }
        
        public IEnumerable<TEntity> GetAll()
        {
            return this.CreateQuery().ToArray();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this.CreateQuery()
                .ToArrayAsync()
                .ConfigureAwait(false);
        }

        public IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            return this.BuildIncludes(includes).ToArray();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            return await this.BuildIncludes(includes).ToArrayAsync();
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return this.CreateQuery()
                .Where(predicate)
                .ToArray();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.CreateQuery()
                .Where(predicate)
                .ToArrayAsync()
                .ConfigureAwait(false);
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return this.BuildIncludes(includes)
                .Where(predicate)
                .ToArray();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, 
            params Expression<Func<TEntity, object>>[] includes)
        {
            return await this.BuildIncludes(includes)
                .Where(predicate)
                .ToArrayAsync()
                .ConfigureAwait(false);
        }

        public TEntity Find(TKey key)
        {
            return this.CreateQuery()
                .FirstOrDefault(entity => entity.Id.Equals(key));
        }

        public async Task<TEntity> FindAsync(TKey key)
        {
            return await this.CreateQuery()
                .FirstOrDefaultAsync(entity => entity.Id.Equals(key))
                .ConfigureAwait(false);
        }

        public TEntity Find(TKey key, params Expression<Func<TEntity, object>>[] includes)
        {
            return this.BuildIncludes(includes)
                .FirstOrDefault(entity => entity.Id.Equals(key));
        }

        public async Task<TEntity> FindAsync(TKey key, params Expression<Func<TEntity, object>>[] includes)
        {
            return await this.BuildIncludes(includes)
                .FirstOrDefaultAsync(entity => entity.Id.Equals(key))
                .ConfigureAwait(false);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return this.CreateQuery().FirstOrDefault(predicate);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.CreateQuery()
                .FirstOrDefaultAsync(predicate)
                .ConfigureAwait(false);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return this.BuildIncludes(includes)
                .FirstOrDefault(predicate);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return await this.BuildIncludes(includes)
                .FirstOrDefaultAsync(predicate)
                .ConfigureAwait(false);
        }

        public void Add(TEntity entity)
        {
            this.context.Set<TEntity>().Add(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            await this.context.Set<TEntity>().AddAsync(entity);
        }

        public void Edit(TEntity entity)
        {
            var entry = this.context.Entry(entity);

            entry.State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            var entry = this.context.Entry(entity);

            entry.State = EntityState.Deleted;
        }

        public bool EnsureChanges()
        {
            var modifiedCount = this.context.SaveChanges();

            return modifiedCount > 0;
        }

        public async Task<bool> EnsureChangesAsync()
        {
            var modifiedCount = await this.context.SaveChangesAsync();

            return modifiedCount > 0;
        }

        public IQueryable<TEntity> GetQueryAble(bool noTracking = false)
        {
            return noTracking
                ? this.CreateQuery().AsNoTracking()
                : this.CreateQuery();
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