using ETDB.API.ServiceBase.Domain.Abstractions.Base;
using System.Linq;

namespace ETDB.API.ServiceBase.Repositories.Abstractions.Generics
{
    public interface IEntityRepository<TEntity> : IReadRepository<TEntity>, IWriteRepository<TEntity> where TEntity : class, IEntity, new()
    {
        IQueryable<TEntity> GetQueryable();
    }
}
