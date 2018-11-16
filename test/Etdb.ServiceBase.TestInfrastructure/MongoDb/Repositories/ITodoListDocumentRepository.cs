using System;
using Etdb.ServiceBase.DocumentRepository.Abstractions;
using Etdb.ServiceBase.TestInfrastructure.MongoDb.Documents;

namespace Etdb.ServiceBase.TestInfrastructure.MongoDb.Repositories
{
    public interface ITodoListDocumentRepository : IDocumentRepository<TodoListDocument, Guid>
    {
    }
}