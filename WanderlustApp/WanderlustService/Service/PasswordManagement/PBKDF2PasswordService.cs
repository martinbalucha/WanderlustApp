using System;
using System.Linq;
using System.Security.Cryptography;

namespace WanderlustService.Service.PasswordManagement
{
    /// <summary>
    /// An implementation of the interface <see cref="IPasswordService"/>
    /// </summary>
    public class PBKDF2PasswordService : IPasswordService
    {
        /// <summary>
        /// The number of PBKDF2 iterations
        /// </summary>
        private const int PBKDF2IterCount = 100000;

        /// <summary>
        /// The length of the PBKDF2 subkey
        /// </summary>
        private const int PBKDF2SubkeyLength = 160 / 8;

        /// <summary>
        /// The size of the salt
        /// </summary>
        private const int SaltSize = 128 / 8;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Tuple<string, string> CreateHash(string password)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, SaltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);

                return Tuple.Create(Convert.ToBase64String(subkey), Convert.ToBase64String(salt));
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>        
        public bool VerifyHashedPassword(string hashedPassword, string salt, string password)
        {
            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);
            var saltBytes = Convert.FromBase64String(salt);

            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, PBKDF2IterCount))
            {
                var generatedSubkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
                return hashedPasswordBytes.SequenceEqual(generatedSubkey);
            }
        }
    }
}
