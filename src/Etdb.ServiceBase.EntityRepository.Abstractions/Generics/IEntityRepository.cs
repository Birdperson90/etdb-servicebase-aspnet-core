using System;
using System.Linq;
using System.Threading.Tasks;
using Etdb.ServiceBase.Domain.Abstractions.Entities;

namespace Etdb.ServiceBase.EntityRepository.Abstractions.Generics
{
    public interface IEntityRepository<TEntity, TId> : IReadEntityRepository<TEntity, TId>, 
        IWriteEntityRepository<TEntity, TId> where TEntity : class, IEntity<TId>, new() where TId : IEquatable<TId>
    {
        IQueryable<TEntity> GetQueryAble(bool noTracking = false);
        
        int Count();

        Task<int> CountAsync();
    }
}