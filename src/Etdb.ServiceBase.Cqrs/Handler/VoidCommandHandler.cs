using System.Threading;
using System.Threading.Tasks;
using Etdb.ServiceBase.Cqrs.Abstractions.Commands;
using Etdb.ServiceBase.Cqrs.Abstractions.Handler;
using Etdb.ServiceBase.Cqrs.Abstractions.Validation;

namespace Etdb.ServiceBase.Cqrs.Handler
{
    public abstract class VoidCommandHandler<TVoidCommand> : IVoidCommandHandler<TVoidCommand>
        where TVoidCommand : class, IVoidCommand
    {
        protected readonly IVoidCommandValidation<TVoidCommand> CommandValidation;

        protected VoidCommandHandler(IVoidCommandValidation<TVoidCommand> commandValidation)
        {
            CommandValidation = commandValidation;
        }

        public abstract Task Handle(TVoidCommand request, CancellationToken cancellationToken);
    }
}