using System.Linq;
using Etdb.ServiceBase.Domain.Abstractions.Base;

namespace Etdb.ServiceBase.Repositories.Abstractions.Generics
{
    public interface IEntityRepository<TEntity> : IReadRepository<TEntity>, IWriteRepository<TEntity> where TEntity : class, IEntity, new()
    {
        IQueryable<TEntity> GetQueryable();
    }
}
