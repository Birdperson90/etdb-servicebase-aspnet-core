using System.Threading;
using System.Threading.Tasks;
using Etdb.ServiceBase.Cqrs.Abstractions.Commands;
using Etdb.ServiceBase.Cqrs.Abstractions.Handler;
using Etdb.ServiceBase.Cqrs.Abstractions.Validation;

namespace Etdb.ServiceBase.Cqrs.Handler
{
    public abstract class
        ValidationResponseCommandHandler<TResponseCommand, TResponse> : IResponseCommandHandler<TResponseCommand,
            TResponse>
        where TResponseCommand : class, IResponseCommand<TResponse> where TResponse : class
    {
        protected readonly ICommandValidation<TResponseCommand> CommandValidation;

        protected ValidationResponseCommandHandler(ICommandValidation<TResponseCommand> commandValidation)
        {
            this.CommandValidation = commandValidation;
        }

        public abstract Task<TResponse> Handle(TResponseCommand request, CancellationToken cancellationToken);
    }
}