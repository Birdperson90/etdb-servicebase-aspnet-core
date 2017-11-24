using ETDB.API.ServiceBase.Domain.Abstractions.Commands;

namespace ETDB.API.ServiceBase.Domain.Abstractions.Base
{
    public interface IUnitOfWork
    {
        CommandResponse Commit();
    }
}