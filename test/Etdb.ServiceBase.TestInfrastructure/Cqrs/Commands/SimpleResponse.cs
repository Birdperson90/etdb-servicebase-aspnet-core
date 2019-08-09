namespace Etdb.ServiceBase.TestInfrastructure.Cqrs.Commands
{
    public class SimpleResponse
    {
        public int ResponseValue { get; }

        public SimpleResponse(int responseValue)
        {
            this.ResponseValue = responseValue;
        }
    }
}