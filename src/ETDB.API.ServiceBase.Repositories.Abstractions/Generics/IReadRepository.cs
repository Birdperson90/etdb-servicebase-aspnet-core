using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ETDB.API.ServiceBase.Domain.Abstractions.Base;

namespace ETDB.API.ServiceBase.Repositories.Abstractions.Generics
{
    public interface IReadRepository<TEntity> where TEntity : class, IEntity, new()
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> GetAllIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        TEntity Get(Guid id);
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        TEntity GetIncluding(Guid id, params Expression<Func<TEntity, object>>[] includes);
        TEntity GetIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
    }
}
