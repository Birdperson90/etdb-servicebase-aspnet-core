using Microsoft.Extensions.Logging;
using Moq;

namespace Etdb.ServiceBase.TestInfrastructure.Mocks
{
    public class LoggerMock<T> where T : class
    {
        private readonly IMock<ILogger<T>> mock;

        public LoggerMock()
        {
            this.mock = new Mock<ILogger<T>>(MockBehavior.Default);
        }

        public ILogger<T> Logger => this.mock.Object;

        public Mock<ILogger<T>> Mock => (Mock<ILogger<T>>) this.mock;
    }
}