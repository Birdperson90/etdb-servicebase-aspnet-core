using System;
using System.Collections.Generic;
using Etdb.ServiceBase.Domain.Abstractions.Documents;
using MongoDB.Bson.Serialization.Attributes;

namespace Etdb.ServiceBase.TestInfrastructure.MongoDb.Documents
{
    public class TodoListDocument : IDocument<Guid>
    {
        public TodoListDocument()
        {
            this.Todos = new List<TodoDocument>();
        }

        [BsonId] public Guid Id { get; set; }

        public string Titel { get; set; } = null!;

        public ICollection<TodoDocument> Todos { get; set; }
    }
}