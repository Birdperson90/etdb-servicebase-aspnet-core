using System;
using System.Threading.Tasks;
using Etdb.ServiceBase.Domain.Abstractions.Entities;

namespace Etdb.ServiceBase.EntityRepository.Abstractions.Generics
{
    public interface IWriteEntityRepository<in TEntity, TId> where TEntity : class, IEntity<TId>, new()
        where TId : IEquatable<TId>
    {
        void Add(TEntity entity);

        Task AddAsync(TEntity entity);

        void Edit(TEntity entity);

        void Delete(TEntity entity);

        bool EnsureChanges();

        Task<bool> EnsureChangesAsync();
    }
}