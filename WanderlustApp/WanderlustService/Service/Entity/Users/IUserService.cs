using System.Threading.Tasks;
using WanderlustPersistence.Entity;
using WanderlustService.Service.Common;

namespace WanderlustService.Service.Entities.Users
{
    /// <summary>
    /// An interface for the service class used for performing business logic
    /// operations on <see cref="User"/> entity
    /// </summary>
    public interface IUserService : IEntityService<User>
    {
        /// <summary>
        /// Attempts to authentiace a user by the 
        /// </summary>
        /// <param name="username">A username for the attempted login</param>
        /// <param name="password">A plaintext password for the attepmted login</param>
        /// <returns></returns>
        Task<bool> AuthenticateAsync(string username, string password);
        
        /// <summary>
        /// Asynchronously changes the user password
        /// </summary>
        /// <param name="username">The username of the user whose password will be changed</param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task ChangePasswordAsync(string username, string oldPassword, string newPassword);

        /// <summary>
        /// Asynchronously finds user by a username which is unique
        /// </summary>
        /// <param name="username">A username of the sought user</param>
        /// <returns>User with the given username. Null, if no such user exists</returns>
        Task<User> FindByUsernameAsync(string username);

        /// <summary>
        /// Asynchronously registers a new user
        /// </summary>
        /// <param name="user">A new user</param>
        /// <returns>Task</returns>
        Task RegisterAsync(User user);
    }
}
