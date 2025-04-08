using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;
using UserService.Interfaces;

namespace UserService.Repositories
{
    public class HashingRepository : IHashingRepository
    {
        private readonly IConfiguration _configuration;

        public HashingRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string HashPassword(string password)
        {
            var hashingConfig = _configuration.GetSection("HashingConfig");

            if (hashingConfig == null)
            {
                throw new ArgumentNullException(nameof(hashingConfig), "Hashing configuration section is missing.");
            }
            var saltSizeValue = hashingConfig.GetRequiredSection("SaltSize").Value;
            var hashSizeValue = hashingConfig.GetRequiredSection("HashSize").Value;
            var iterationsValue = hashingConfig.GetRequiredSection("Iterations").Value;
            var memorySizeValue = hashingConfig.GetRequiredSection("MemorySize").Value;

            if (saltSizeValue == null || hashSizeValue == null || iterationsValue == null || memorySizeValue == null)
            {
                throw new ArgumentNullException("Hashing config is missing stuff in the configuration.");
            }

            int saltSize = int.Parse(saltSizeValue);
            int hashSize = int.Parse(hashSizeValue);
            int iterations = int.Parse(iterationsValue);
            int memorySize = int.Parse(memorySizeValue);

            byte[] bytesPassword = Encoding.UTF8.GetBytes(password);
            byte[] salt = new byte[saltSize];
            RandomNumberGenerator.Fill(salt);

            using (var argon2id = new Argon2id(bytesPassword))
            {
                argon2id.Salt = salt;
                argon2id.MemorySize = memorySize;
                argon2id.Iterations = iterations;
                argon2id.DegreeOfParallelism = 1;

                byte[] hash = argon2id.GetBytes(hashSize);
                byte[] hashBytes = new byte[saltSize + hashSize];
                Array.Copy(salt, 0, hashBytes, 0, saltSize);
                Array.Copy(hash, 0, hashBytes, saltSize, hashSize);

                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
