using System.Threading;
using System.Threading.Tasks;
using Etdb.ServiceBase.Cqrs.Abstractions.Commands;
using Etdb.ServiceBase.Cqrs.Abstractions.Handler;
using Etdb.ServiceBase.Cqrs.Abstractions.Validation;

namespace Etdb.ServiceBase.Cqrs.Handler
{
    public abstract class
        ResponseCommandHandler<TResponseCommand, TResponse> : IResponseCommandHandler<TResponseCommand, TResponse>
        where TResponseCommand : class, IResponseCommand<TResponse>
    {
        protected readonly ICommandValidation<TResponseCommand> CommandValidation;

        protected ResponseCommandHandler(ICommandValidation<TResponseCommand> commandValidation)
        {
            this.CommandValidation = commandValidation;
        }

        public abstract Task<TResponse> Handle(TResponseCommand request, CancellationToken cancellationToken);
    }
}