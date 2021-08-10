using GET.Core.Application.Contracts;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace GET.Core.Application.Features
{
    public class Hasher : IHasher
    {
        public string Hash(string plainText)
        {
            var saltBytes = Generate128BitSalt();

            var hashBytes = KeyDerivation.Pbkdf2(password: plainText, salt: saltBytes, prf: KeyDerivationPrf.HMACSHA1, iterationCount: 10000, numBytesRequested: 256 / 8);
            return string.Concat(Convert.ToBase64String(saltBytes), " ", Convert.ToBase64String(hashBytes));
        }

        public bool Verify(string plainText, string hash)
        {
            var splitHash = hash.Split(' ');
            var saltBytes = Convert.FromBase64String(splitHash[0]);
            var hashBytes = Convert.FromBase64String(splitHash[1]);

            var newHashBytes = KeyDerivation.Pbkdf2(password: plainText, salt: saltBytes, prf: KeyDerivationPrf.HMACSHA1, iterationCount: 10000, numBytesRequested: 256 / 8);

            return hashBytes.SequenceEqual(newHashBytes);
        }

        private byte[] Generate128BitSalt()
        {
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
    }
}
