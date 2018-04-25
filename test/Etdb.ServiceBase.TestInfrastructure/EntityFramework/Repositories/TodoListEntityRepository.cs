using System;
using Etdb.ServiceBase.EntityRepository.Abstractions.Context;
using Etdb.ServiceBase.EntityRepository.Generics;
using Etdb.ServiceBase.TestInfrastructure.EntityFramework.Entities;

namespace Etdb.ServiceBase.TestInfrastructure.EntityFramework.Repositories
{
    public class TodoListEntityRepository : GenericEntityRepository<TodoListEntity, Guid>, ITodoListEntityRepository
    {
        public TodoListEntityRepository(EntityDbContext context) : base(context)
        {
        }
    }
}