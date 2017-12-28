using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Etdb.ServiceBase.Domain.Abstractions.Base;
using Etdb.ServiceBase.Repositories.Abstractions.Base;
using Etdb.ServiceBase.Repositories.Abstractions.Generics;
using MongoDB.Driver;

namespace Etdb.ServiceBase.Repositories.Generics
{
    public abstract class EntityRepository<TEntity> : IDisposable, IEntityRepository<TEntity> where TEntity : class, IEntity
    {
        private AppContextBase dbContext;

        protected EntityRepository(AppContextBase dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(string collectionName = null)
        {
            var result = await this.GetCollection(collectionName)
                .AsQueryable()
                .ToListAsync();

            return result;
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, string collectionName = null)
        {
            var result = this.GetCollection(collectionName)
                .AsQueryable()
                .Where(predicate)
                .ToList();

            return result;
        }

        public async Task<TEntity> GetAsync(Guid id, string collectionName = null)
        {
            var result = await this.GetCollection(collectionName)
                .Find(entity => entity.Id == id)
                .SingleAsync();

            return result;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, string collectionName = null)
        {
            var result = await this.GetCollection(collectionName)
                .Find(predicate)
                .SingleAsync();

            return result;
        }

        public async Task<IQueryable<TEntity>> GetQueryableAsync(string collectionName = null)
        {
            var result = await this.GetCollection(collectionName)
                .AsQueryable()
                .ToListAsync();

            return result.AsQueryable();
        }

        public async Task AddAsync(TEntity entity, string collectionName = null)
        {
            AddIdentifier(entity);
            await this.GetCollection(collectionName).InsertOneAsync(entity);
        }

        public async Task<bool> EditAsync(TEntity entity, string collectionName = null)
        {
            var result = await this.GetCollection(collectionName).UpdateOneAsync(existingEntity => existingEntity.Id == entity.Id,
                new ObjectUpdateDefinition<TEntity>(entity));

            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(Guid id, string collectionName = null)
        {
            var result = await this.GetCollection(collectionName)
                .DeleteOneAsync(existingEntity => existingEntity.Id == id);

            return result.DeletedCount > 0;
        }

        private IMongoCollection<TEntity> GetCollection(string collectionName)
        {
            return collectionName == null
                ? this.dbContext.GetCollection<TEntity>()
                : this.dbContext.GetCollection<TEntity>(collectionName);
        }

        private static void AddIdentifier(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
        }

        public void Dispose()
        {
            this.dbContext = null;
        }
    }
}
