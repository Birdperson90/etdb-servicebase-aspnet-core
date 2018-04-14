using Xunit;

namespace Etdb.ServiceBase.Builder.IntegrationTests
{
    public class TestClass
    {
        [Fact]
        public void TestSmth()
        {
            const int x = 2 * 2;
            Assert.Equal(4, x);
        }
    }
}