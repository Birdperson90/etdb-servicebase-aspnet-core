using System;
using System.Threading.Tasks;
using Etdb.ServiceBase.EntityDomain.Abstractions;

namespace Etdb.ServiceBase.EntityRepository.Abstractions.Generics
{
    public interface IWriteEntityRepository<in TEntity, TKey> where TEntity : class, IEntity<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        void Add(TEntity entity);

        Task AddAsync(TEntity entity);

        void Edit(TEntity entity);

        void Delete(TEntity entity);

        bool EnsureChanges();

        Task<bool> EnsureChangesAsync();
    }
}