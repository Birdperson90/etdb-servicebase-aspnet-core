using Etdb.ServiceBase.Cqrs.Abstractions.Commands;
using MediatR.Pipeline;

namespace Etdb.ServiceBase.Cqrs.Abstractions.Processor
{
    public interface IVoidCommandPreProcessor<in TVoidCommand> : IRequestPreProcessor<TVoidCommand>
        where TVoidCommand : class, IVoidCommand
    {
        
    }
}