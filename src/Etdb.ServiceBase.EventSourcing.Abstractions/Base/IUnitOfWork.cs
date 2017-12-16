using Etdb.ServiceBase.EventSourcing.Abstractions.Commands;

namespace Etdb.ServiceBase.EventSourcing.Abstractions.Base
{
    public interface IUnitOfWork
    {
        SourcingCommandResponse Commit();
    }
}