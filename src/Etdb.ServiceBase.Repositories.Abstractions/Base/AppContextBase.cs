using Etdb.ServiceBase.Domain.Abstractions.Base;
using MongoDB.Driver;

namespace Etdb.ServiceBase.Repositories.Abstractions.Base
{
    public abstract class AppContextBase
    {
        public abstract IMongoDatabase Database { get; }

        public abstract IMongoCollection<TEntity> GetCollection<TEntity>() where TEntity : class, IEntity;

        public abstract IMongoCollection<TEntity> GetCollection<TEntity>(string collectionName)
            where TEntity : class, IEntity;
    }
}
