using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;

namespace SNServiceAPI.Services
{
    public class HashingService : IHashingService
    {
        IConfiguration Configuration;
        public HashingService(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public bool CompareHashPassword(string plainPassword, string hash)
        {
            throw new System.NotImplementedException();
        }

        public string HashPassword(string plainPassword)
        {
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: plainPassword,
                salt: Encoding.UTF8.GetBytes(Configuration["Salt"]),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
