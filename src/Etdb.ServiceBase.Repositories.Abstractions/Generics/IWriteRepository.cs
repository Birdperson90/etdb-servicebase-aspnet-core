using System;
using System.Threading.Tasks;
using Etdb.ServiceBase.Domain.Abstractions.Base;

namespace Etdb.ServiceBase.Repositories.Abstractions.Generics
{
    public interface IWriteRepository<in TEntity> where TEntity: class, IEntity
    {
        Task AddAsync(TEntity entity, string collectionName = null);
        Task<bool> EditAsync(TEntity entity, string collectionName = null);
        Task<bool> DeleteAsync(Guid id, string collectionName = null);
    }
}
