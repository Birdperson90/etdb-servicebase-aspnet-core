namespace ETDB.API.ServiceBase.EventSourcing.Abstractions.Commands
{
    public class SourcingCommandResponse
    {
        public static SourcingCommandResponse Ok = new SourcingCommandResponse { Success = true };
        public static SourcingCommandResponse Fail = new SourcingCommandResponse { Success = false };

        public SourcingCommandResponse(bool success = false)
        {
            Success = success;
        }

        public bool Success { get; private set; }
    }
}
