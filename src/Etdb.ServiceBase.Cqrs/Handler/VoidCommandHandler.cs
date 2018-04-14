using System.Threading;
using System.Threading.Tasks;
using Etdb.ServiceBase.Cqrs.Abstractions.Commands;
using Etdb.ServiceBase.Cqrs.Abstractions.Handler;

namespace Etdb.ServiceBase.Cqrs.Handler
{
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