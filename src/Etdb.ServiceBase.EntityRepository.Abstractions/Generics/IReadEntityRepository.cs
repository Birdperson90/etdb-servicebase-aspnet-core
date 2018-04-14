using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Etdb.ServiceBase.EntityDomain.Abstractions;

namespace Etdb.ServiceBase.EntityRepository.Abstractions.Generics
{
    public interface IReadEntityRepository<TEntity, in TKey> where TEntity : class, IEntity<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        IEnumerable<TEntity> GetAll();
        
        Task<IEnumerable<TEntity>> GetAllAsync();
        
        IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes);

        Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);

        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes);

        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes);

        TEntity Find(TKey key);

        Task<TEntity> FindAsync(TKey key);
        
        TEntity Find(TKey key, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> FindAsync(TKey key, params Expression<Func<TEntity, object>>[] includes);

        TEntity Find(Expression<Func<TEntity, bool>> predicate);
        
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes);
    }
}