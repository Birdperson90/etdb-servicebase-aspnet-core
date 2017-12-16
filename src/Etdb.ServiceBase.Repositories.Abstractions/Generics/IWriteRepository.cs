using System.Threading.Tasks;
using Etdb.ServiceBase.Domain.Abstractions.Base;

namespace Etdb.ServiceBase.Repositories.Abstractions.Generics
{
    public interface IWriteRepository<in TEntity> where TEntity: class, IEntity, new()
    {
        void Add(TEntity entity);
        void Edit(TEntity entity);
        void Delete(TEntity entity);
        int EnsureChanges();
        Task<int> EnsureChangesAsync();
    }
}
