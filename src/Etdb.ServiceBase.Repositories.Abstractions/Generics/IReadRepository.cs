using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Etdb.ServiceBase.Domain.Abstractions.Base;

namespace Etdb.ServiceBase.Repositories.Abstractions.Generics
{
    public interface IReadRepository<TEntity> where TEntity : class, IEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync(string collectionName = null);

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, string collectionName = null);

        Task<TEntity> GetAsync(Guid id, string collectionName = null);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, string collectionName = null);

        Task<IQueryable<TEntity>> GetQueryableAsync(string collectionName = null);
    }
}
