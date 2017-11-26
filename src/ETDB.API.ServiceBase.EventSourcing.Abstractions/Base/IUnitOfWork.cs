using ETDB.API.ServiceBase.EventSourcing.Abstractions.Commands;

namespace ETDB.API.ServiceBase.EventSourcing.Abstractions.Base
{
    public interface IUnitOfWork
    {
        SourcingCommandResponse Commit();
    }
}