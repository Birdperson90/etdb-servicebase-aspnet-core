namespace Etdb.ServiceBase.EventSourcing.Abstractions.Base
{
    public interface IEventUser
    {
        string UserName { get; }
        bool IsAuthenticated();
    }
}