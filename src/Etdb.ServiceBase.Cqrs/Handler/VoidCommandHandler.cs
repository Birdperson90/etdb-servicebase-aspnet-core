using System.Threading;
using System.Threading.Tasks;
using Etdb.ServiceBase.Cqrs.Abstractions.Commands;
using Etdb.ServiceBase.Cqrs.Abstractions.Handler;
using Etdb.ServiceBase.Cqrs.Abstractions.Validation;
using MediatR;

namespace Etdb.ServiceBase.Cqrs.Handler
{
    public abstract class VoidCommandHandler<TVoidCommand> : IVoidCommandHandler<TVoidCommand>
        where TVoidCommand : class, IVoidCommand
    {
        protected readonly ICommandValidation<TVoidCommand> CommandValidation;

        protected VoidCommandHandler(ICommandValidation<TVoidCommand> commandValidation)
        {
            this.CommandValidation = commandValidation;
        }

        public abstract Task<Unit> Handle(TVoidCommand request, CancellationToken cancellationToken);
    }
}