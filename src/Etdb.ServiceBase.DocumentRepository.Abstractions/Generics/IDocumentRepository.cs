using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Transactions;
using Etdb.ServiceBase.DocumentDomain.Abstractions;

namespace Etdb.ServiceBase.DocumentRepository.Abstractions.Generics
{
    public interface IDocumentRepository<TDocument, in TKey> : IReadDocumentRepository<TDocument, TKey>, 
        IWriteDocumentRepository<TDocument, TKey> where TDocument : class, IDocument<TKey> where TKey : IEquatable<TKey>
    {
    }
}