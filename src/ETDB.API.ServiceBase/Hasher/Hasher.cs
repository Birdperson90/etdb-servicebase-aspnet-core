using System;
using System.Security.Cryptography;
using ETDB.API.ServiceBase.Abstractions.Hasher;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ETDB.API.ServiceBase.Hasher
{
    public class Hasher : IHasher
    {
        public string CreateSaltedHash(string unhashed, byte[] salt)
        {
            if (unhashed == null) throw new ArgumentNullException(nameof(unhashed));

            if (salt == null) throw new ArgumentNullException(nameof(salt));

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(unhashed, 
                salt, KeyDerivationPrf.HMACSHA1, 10000, 256 / 8));
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
