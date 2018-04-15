using System;
using Etdb.ServiceBase.DocumentDomain.Abstractions;

namespace Etdb.ServiceBase.TestInfrastructure.MongoDb.Documents
{
    public class TodoDocument : IDocument<Guid>
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public int Prio { get; set; }

        public bool Done { get; set; }

        public TodoListDocument List { get; set; }

        public Guid ListId { get; set; }
    }
}