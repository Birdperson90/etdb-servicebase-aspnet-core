using System.Threading;
using System.Threading.Tasks;
using Etdb.ServiceBase.Cqrs.Abstractions.Commands;
using Etdb.ServiceBase.Cqrs.Abstractions.Handler;
using Etdb.ServiceBase.Cqrs.Abstractions.Validation;

namespace Etdb.ServiceBase.Cqrs.Handler
{
    public abstract class ResponseCommandHandler<TResponseCommand, TResponse> : IResponseCommandHandler<TResponseCommand, TResponse>
        where TResponseCommand : class, IResponseCommand<TResponse>
    {
        protected readonly IResponseCommandValidation<TResponseCommand, TResponse> CommandValidation;

        protected ResponseCommandHandler(IResponseCommandValidation<TResponseCommand, TResponse> commandValidation)
        {
            CommandValidation = commandValidation;
        }

        public abstract Task<TResponse> Handle(TResponseCommand request, CancellationToken cancellationToken);
    }
    
    public abstract class VoidCommandHandler<TVoidCommand> : IVoidCommandHandler<TVoidCommand>
        where TVoidCommand : class, IVoidCommand
    {
        protected readonly IVoidCommandHandler<TVoidCommand> CommandValidation;

        protected VoidCommandHandler(IVoidCommandHandler<TVoidCommand> commandValidation)
        {
            CommandValidation = commandValidation;
        }

        public abstract Task Handle(TVoidCommand request, CancellationToken cancellationToken);
    }
}