using System;
using Etdb.ServiceBase.DocumentRepository;
using Etdb.ServiceBase.TestInfrastructure.MongoDb.Documents;

namespace Etdb.ServiceBase.TestInfrastructure.MongoDb.Repositories
{
    public class TodoListDocumentRepository : GenericDocumentRepository<TodoListDocument, Guid>,
        ITodoListDocumentRepository
    {
        public TodoListDocumentRepository(DocumentDbContext context) : base(context)
        {
        }
    }
}