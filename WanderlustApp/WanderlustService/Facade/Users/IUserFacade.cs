using System.Threading.Tasks;
using WanderlustService.DataTransferObject.Entities.User;

namespace WanderlustService.Facade.Users
{
    /// <summary>
    /// An interface for the user facade
    /// </summary>
    public interface IUserFacade
    {
        /// <summary>
        /// Asynchronously attempts to authentize a user
        /// </summary>
        /// <param name="userDto">A DTO for a user attempting to log in</param>
        /// <returns>True if the authentication was successful. Otherwise false</returns>
        Task<bool> AuthentizeAsync(UserLoginDto userDto);

        /// <summary>
        /// Asynchronously attempts to change user's password
        /// </summary>
        /// <param name="userDto">A DTO containg needed information for a password change</param>
        /// <returns>Task</returns>
        Task ChangePasswordAsync(UserPasswordChangeDto userDto);

        /// <summary>
        /// Asynchronously registers a new user
        /// </summary>
        /// <param name="userDto">A DTO with information required for a registration of a new user</param>
        /// <returns>Task</returns>
        Task RegisterAsync(UserRegisterDto userDto);

        /// <summary>
        /// Asynchronously updates a user
        /// </summary>
        /// <param name="userDto">A DTO containing updated information</param>
        /// <returns>Task</returns>
        Task UpdateAsync(UserUpdateDto userDto);

        /// <summary>
        /// Asynchronously finds the user with the given name
        /// </summary>
        /// <param name="username">The name of the sought user</param>
        /// <returns>The user who has the given username. Null, if no such user exists</returns>
        Task<UserDto> FindByUsernameAsync(string username);
    }
}
