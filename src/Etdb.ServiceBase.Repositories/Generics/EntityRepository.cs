using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Etdb.ServiceBase.Domain.Abstractions.Base;
using Etdb.ServiceBase.Repositories.Abstractions.Base;
using Etdb.ServiceBase.Repositories.Abstractions.Generics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace Etdb.ServiceBase.Repositories.Generics
{
    public abstract class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        protected readonly AppContextBase Context;

        protected EntityRepository(AppContextBase context)
        {
            this.Context = context;
        }

        public virtual void Add(TEntity entity)
        {
            this.Context.Set<TEntity>().Add(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            var entry = this.Context.Entry(entity);
            entry.State = EntityState.Deleted;
        }

        public virtual void Edit(TEntity entity)
        {
            var entry = this.Context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return await this.BuildQuery(includes)
                .Where(predicate)
                .ToListAsync();
        }

        public virtual TEntity Get(Guid id)
        {
            var entity = this
                .GetQueryable()
                .FirstOrDefault(data => data.Id == id);

            return entity;
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = this.GetQueryable()
                .Where(predicate)
                .FirstOrDefault();

            return entity;
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var entity = this.BuildQuery(includes)
                .Where(predicate)
                .FirstOrDefault();


            return entity;
        }

        public virtual TEntity Get(Guid id, params Expression<Func<TEntity, object>>[] includes)
        {
            var entity = this.BuildQuery(includes)
                .FirstOrDefault(data => data.Id == id);

            return entity;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return this.GetQuery()
                .ToArray();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this.GetQuery()
                .ToListAsync();
        }

        public virtual IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            return this.BuildQuery(includes)
                .ToArray();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            return await this.BuildQuery(includes)
                .ToListAsync();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return this.BuildQuery(includes)
                .Where(predicate)
                .ToArray();
        }

        public virtual IQueryable<TEntity> GetQueryable()
        {
            var query = this.Context
                .Set<TEntity>()
                .AsQueryable();

            return query;
        }

        public virtual int EnsureChanges()
        {
            return this.Context.SaveChanges();
        }

        public virtual async Task<int> EnsureChangesAsync()
        {
            return await this.Context.SaveChangesAsync();
        }

        private IQueryable<TEntity> BuildQuery(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = this.GetQuery();

            return includes.Aggregate(query, (current, include) => current.Include(include));
        }

        private IQueryable<TEntity> GetQuery()
        {
            var query = this.Context
                .Set<TEntity>()
                .AsQueryable();

            return query;
        }

        public void Dispose()
        {
            this.Context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
