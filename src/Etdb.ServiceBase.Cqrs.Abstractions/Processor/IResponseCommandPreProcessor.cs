using Etdb.ServiceBase.Cqrs.Abstractions.Commands;
using MediatR.Pipeline;

namespace Etdb.ServiceBase.Cqrs.Abstractions.Processor
{
    public interface IResponseCommandPreProcessor<in TResponseCommand, TResponse> : IRequestPreProcessor<TResponseCommand>
        where TResponseCommand : class, IResponseCommand<TResponse>
    {
        
    }
}