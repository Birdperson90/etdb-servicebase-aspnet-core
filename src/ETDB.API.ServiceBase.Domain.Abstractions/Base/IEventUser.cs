namespace ETDB.API.ServiceBase.Domain.Abstractions.Base
{
    public interface IEventUser
    {
        string UserName { get; }
        bool IsAuthenticated();
    }
}