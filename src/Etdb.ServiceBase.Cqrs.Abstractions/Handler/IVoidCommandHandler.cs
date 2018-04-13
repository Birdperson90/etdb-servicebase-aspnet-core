using Etdb.ServiceBase.Cqrs.Abstractions.Commands;
using MediatR;

namespace Etdb.ServiceBase.Cqrs.Abstractions.Handler
{
    public interface IVoidCommandHandler<in TVoidCommand> : IRequestHandler<TVoidCommand> where TVoidCommand : class, IVoidCommand
    {
        
    }
}