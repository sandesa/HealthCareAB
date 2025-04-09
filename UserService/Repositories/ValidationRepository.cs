using Konscious.Security.Cryptography;
using System.Text;
using UserService.Interfaces;

namespace UserService.Repositories
{
    public class ValidationRepository : IValidationRepository
    {
        private readonly IConfiguration _configuration;

        public ValidationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool VerifyPassword(string password, string storedHash)
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

            byte[] tryPasswordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = Convert.FromBase64String(storedHash);
            byte[] salt = new byte[saltSize];
            Array.Copy(hashBytes, 0, salt, 0, saltSize);
            byte[] storedPasswordHash = new byte[hashSize];
            Array.Copy(hashBytes, saltSize, storedPasswordHash, 0, hashSize);

            using (var argon2id = new Argon2id(tryPasswordBytes))
            {
                argon2id.Salt = salt;
                argon2id.MemorySize = memorySize;
                argon2id.Iterations = iterations;
                argon2id.DegreeOfParallelism = 1;
                byte[] tryPasswordHash = argon2id.GetBytes(hashSize);
                return CompareByteArrays(tryPasswordHash, storedPasswordHash);
            }
        }

        public bool CompareByteArrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
