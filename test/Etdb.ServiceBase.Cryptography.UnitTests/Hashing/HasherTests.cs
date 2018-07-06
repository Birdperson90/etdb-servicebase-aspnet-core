using System;
using Etdb.ServiceBase.Cryptography.Abstractions.Hashing;
using Etdb.ServiceBase.Cryptography.Hashing;
using Xunit;

namespace Etdb.ServiceBase.Cryptography.UnitTests.Hashing
{
    public class HasherTests
    {
        private readonly IHasher hasher;
        private const string SamplePassword = "abcdefg";

        public HasherTests()
        {
            this.hasher = new Hasher();
        }

        [Fact]
        public void Hasher_CreateSaltedHashWithSameHashSamePasswordHashTwice_ExpectHashesToBeEqual()
        {
            var salt = this.hasher.GenerateSalt();

            var hashOne = this.hasher.CreateSaltedHash(SamplePassword, salt);

            var hashTwo = this.hasher.CreateSaltedHash(SamplePassword, salt);

            Assert.Equal(hashOne, hashTwo);
        }

        [Fact]
        public void Hasher_CreateSaltedHashWithDifferentHashesSamePasswordHashTwice_ExpectHashesNotToBeEqual()
        {
            var saltOne = this.hasher.GenerateSalt();
            var saltTwo = this.hasher.GenerateSalt();

            var hashOne = this.hasher.CreateSaltedHash(SamplePassword, saltOne);

            var hashTwo = this.hasher.CreateSaltedHash(SamplePassword, saltTwo);

            Assert.NotEqual(hashOne, hashTwo);
        }

        [Fact]
        public void Hasher_CreateSaltedHashWithNullValues_ExpectExceptions()
        {
            var salt = this.hasher.GenerateSalt();

            Assert.Throws<ArgumentException>(() => this.hasher.CreateSaltedHash(null, salt));
            Assert.Throws<ArgumentException>(() => this.hasher.CreateSaltedHash(string.Empty, salt));
            Assert.Throws<ArgumentNullException>(() => this.hasher.CreateSaltedHash(SamplePassword, null));
        }

        [Fact]
        public void Hasher_GenerateSalt_ExpectByteArray()
        {
            var salt = this.hasher.GenerateSalt();
            Assert.Equal(16, salt.Length);
        }
    }
}