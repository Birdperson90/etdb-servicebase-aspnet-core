using System.Threading.Tasks;
using ETDB.API.ServiceBase.Domain.Abstractions.Base;

namespace ETDB.API.ServiceBase.Repositories.Abstractions.Generics
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
