using Microsoft.AspNetCore.Hosting;
using Moq;

namespace Etdb.ServiceBase.TestInfrastructure.Mocks
{
    public class HostingEnvironmentMock
    {
        private readonly IMock<IWebHostEnvironment> mock;

        public HostingEnvironmentMock()
        {
            this.mock = new Mock<IWebHostEnvironment>(MockBehavior.Default);
        }

        public Mock<IWebHostEnvironment> Mock => (Mock<IWebHostEnvironment>) this.mock;

        public IWebHostEnvironment Environment => this.mock.Object;
    }
}