using System;
using System.Collections.Generic;
using System.Text;
using ETDB.API.ServiceBase.Common.Factory;
using ETDB.API.ServiceBase.Common.Hasher;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ETDB.API.ServiceBase.Common.UnitTests.Factory
{
    [TestClass]
    public class HasherFactoryTests
    {

        [TestMethod]
        public void HasherFactory_Hcma1_ExpectToReceiveInstance()
        {
            var hashingStrategy = new HasherFactory()
                .CreateHasher(KeyDerivationPrf.HMACSHA1);

            Assert.IsNotNull(hashingStrategy);
            Assert.IsTrue(hashingStrategy.GetType().IsAssignableFrom(typeof(Hmacsha1HashingStrategy)));
        }
    }
}
