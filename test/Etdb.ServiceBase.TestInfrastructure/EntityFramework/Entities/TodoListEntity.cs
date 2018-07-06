using System;
using System.Collections.Generic;
using Etdb.ServiceBase.Domain.Abstractions.Entities;

namespace Etdb.ServiceBase.TestInfrastructure.EntityFramework.Entities
{
    public class TodoListEntity : IEntity<Guid>
    {
        public TodoListEntity()
        {
            this.Todos = new List<TodoEntity>();
        }

        public Guid Id { get; set; }

        public string Titel { get; set; }

        public ICollection<TodoEntity> Todos { get; set; }
    }
}