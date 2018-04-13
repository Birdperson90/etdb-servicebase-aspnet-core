using MediatR;

namespace Etdb.ServiceBase.Cqrs.Abstractions.Commands
{
    public interface IResponseCommand<out TResponse> : IRequest<TResponse>
    {
        
    }
}