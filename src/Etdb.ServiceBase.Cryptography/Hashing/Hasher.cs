using System;
using System.Security.Cryptography;
using Etdb.ServiceBase.Cryptography.Abstractions.Hashing;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Etdb.ServiceBase.Cryptography.Hashing
{
    public class Hasher : IHasher
    {
        public string CreateSaltedHash(string unhashed, byte[] salt)
        {
            if (unhashed == null || string.IsNullOrWhiteSpace(unhashed))
            {
                throw new ArgumentException(nameof(unhashed));
            }

            if (salt == null) throw new ArgumentNullException(nameof(salt));

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(unhashed,
                salt, KeyDerivationPrf.HMACSHA512, 10000, 256 / 8));
        }

        public byte[] GenerateSalt()
        {
            var salt = new byte[128 / 8];

            using (var keyGenerator = RandomNumberGenerator.Create())
            {
                keyGenerator.GetBytes(salt);
            }

            return salt;
        }
    }
}