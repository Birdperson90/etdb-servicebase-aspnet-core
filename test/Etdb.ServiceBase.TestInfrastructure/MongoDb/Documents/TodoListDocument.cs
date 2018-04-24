using System;
using System.Collections.Generic;
using Etdb.ServiceBase.Domain.Abstractions.Documents;

namespace Etdb.ServiceBase.TestInfrastructure.MongoDb.Documents
{
    public class TodoListDocument : IDocument<Guid>
    {
        public TodoListDocument()
        {
            this.Todos = new List<TodoDocument>();
        }
        
        public Guid Id { get; set; }
        
        public string Titel { get; set; }
        
        public ICollection<TodoDocument> Todos { get; set; }
    }
}