using System;

namespace WanderlustService.Service.PasswordManagement
{
    /// <summary>
    /// A contract for the password management service
    /// </summary>
    public interface IPasswordService
    {
        /// <summary>
        /// Creates hash from the password
        /// </summary>
        /// <param name="password">Plaintext password</param>
        /// <returns>A tuple containing (subkey, salt)</returns>
        Tuple<string, string> CreateHash(string password);

        /// <summary>
        /// Checks whether the hashed password in the database is the same as the hashed password
        /// given at the log in attempt
        /// </summary>
        /// <param name="hashedPassword">A hashed password stored in the database</param>
        /// <param name="salt">Salt stored in the database</param>
        /// <param name="password">Plaintext password</param>
        /// <returns>True if the hashes are the same. Otherwise false</returns>
        bool VerifyHashedPassword(string hashedPassword, string salt, string password);
    }
}
