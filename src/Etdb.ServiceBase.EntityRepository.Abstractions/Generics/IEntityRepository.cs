using System;
using System.Linq;
using System.Threading.Tasks;
using Etdb.ServiceBase.EntityDomain.Abstractions;

namespace Etdb.ServiceBase.EntityRepository.Abstractions.Generics
{
    public interface IEntityRepository<TEntity, TKey> : IReadEntityRepository<TEntity, TKey>, 
        IWriteEntityRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>, new() where TKey : IEquatable<TKey>
    {
        IQueryable<TEntity> GetQueryAble(bool noTracking = false);
        
        int Count();

        Task<int> CountAsync();
    }
}