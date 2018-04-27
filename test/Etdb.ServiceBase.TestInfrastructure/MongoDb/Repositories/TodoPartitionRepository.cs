using System;
using Etdb.ServiceBase.DocumentRepository.Abstractions.Context;
using Etdb.ServiceBase.DocumentRepository.Generics;
using Etdb.ServiceBase.TestInfrastructure.MongoDb.Documents;

namespace Etdb.ServiceBase.TestInfrastructure.MongoDb.Repositories
{
    public class TodoPartitionRepository : GenericDocumentRepository<TodoPartitionDocument, Guid>
    {
        public TodoPartitionRepository(DocumentDbContext context) : base(context)
        {
        }
    }
}