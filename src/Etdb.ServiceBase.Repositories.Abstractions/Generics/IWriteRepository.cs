using System;
using System.Threading.Tasks;
using Etdb.ServiceBase.Domain.Abstractions.Base;

namespace Etdb.ServiceBase.Repositories.Abstractions.Generics
{
    public interface IWriteRepository<in TEntity> where TEntity: class, IEntity
    {
        Task Add(TEntity entity, string collectionName = null);
        Task<bool> Edit(TEntity entity, string collectionName = null);
        Task<bool> Delete(Guid id, string collectionName = null);
    }
}
