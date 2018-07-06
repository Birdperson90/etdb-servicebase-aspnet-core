using Microsoft.Extensions.Hosting;
using Moq;

namespace Etdb.ServiceBase.TestInfrastructure.Mocks
{
    public class HostingEnvironmentMock
    {
        private readonly IMock<IHostingEnvironment> mock;

        public HostingEnvironmentMock()
        {
            this.mock = new Mock<IHostingEnvironment>(MockBehavior.Default);
        }

        public Mock<IHostingEnvironment> Mock => (Mock<IHostingEnvironment>) this.mock;

        public IHostingEnvironment Environment => this.mock.Object;
    }
}