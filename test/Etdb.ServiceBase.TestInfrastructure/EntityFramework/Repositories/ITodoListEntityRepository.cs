using System;
using Etdb.ServiceBase.EntityRepository.Abstractions.Generics;
using Etdb.ServiceBase.TestInfrastructure.EntityFramework.Entities;

namespace Etdb.ServiceBase.TestInfrastructure.EntityFramework.Repositories
{
    public interface ITodoListEntityRepository : IEntityRepository<TodoListEntity, Guid>
    {
    }
}