using Etdb.ServiceBase.Cqrs.Abstractions.Commands;
using MediatR;

namespace Etdb.ServiceBase.Cqrs.Abstractions.Handler
{
    public interface IResponseCommandHandler<in TResponseCommand, TResponse> : IRequestHandler<TResponseCommand, TResponse>
        where TResponseCommand : class, IResponseCommand<TResponse>
    {
        
    }
}