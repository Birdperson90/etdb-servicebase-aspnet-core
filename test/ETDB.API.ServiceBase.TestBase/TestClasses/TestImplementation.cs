using System;

namespace ETDB.API.ServiceBase.TestBase.TestClasses
{
    public class TestImplementation : ITestInterface
    {
        public void DoSomething()
        {
            throw new NotImplementedException();
        }
    }
}
