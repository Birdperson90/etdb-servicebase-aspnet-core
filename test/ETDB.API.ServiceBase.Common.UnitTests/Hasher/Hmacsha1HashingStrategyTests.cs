using System;
using System.Collections.Generic;
using System.Text;
using ETDB.API.ServiceBase.Common.Hasher;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ETDB.API.ServiceBase.Common.UnitTests.Hasher
{
    [TestClass]
    public class Hmacsha1HashingStrategyTests
    {
        [TestMethod]
        public void Hasher_CreatHashTwiceSameSalt_ExpectValuesToBeEqual()
        {
            var hasher = new Hmacsha1HashingStrategy();
            var salt = hasher.GenerateSalt();
            var firstHash = hasher.CreateSaltedHash("Password", salt);
            
            Assert.IsNotNull(firstHash);

            var secondHash = hasher.CreateSaltedHash("Password", salt);

            Assert.IsNotNull(secondHash);
            Assert.AreEqual(firstHash, secondHash);
        }

        [TestMethod]
        public void Hasher_CreatHashTwiceDifferentSalt_ExpectValuesNotToBeEqual()
        {
            var hasher = new Hmacsha1HashingStrategy();
            var firstSalt = hasher.GenerateSalt();
            var firstHash = hasher.CreateSaltedHash("Password", firstSalt);

            Assert.IsNotNull(firstHash);

            var secondSalt = hasher.GenerateSalt();
            var secondHash = hasher.CreateSaltedHash("Password", secondSalt);

            Assert.IsNotNull(secondHash);
            Assert.AreNotEqual(firstHash, secondHash);
        }
    }
}
