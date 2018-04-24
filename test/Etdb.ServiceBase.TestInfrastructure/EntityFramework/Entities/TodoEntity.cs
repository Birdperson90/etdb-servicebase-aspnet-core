using System;
using Etdb.ServiceBase.Domain.Abstractions.Entities;

namespace Etdb.ServiceBase.TestInfrastructure.EntityFramework.Entities
{
    public class TodoEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }

        public int Prio { get; set; }

        public bool Done { get; set; }

        public TodoListEntity List { get; set; }

        public Guid ListId { get; set; }
    }
}