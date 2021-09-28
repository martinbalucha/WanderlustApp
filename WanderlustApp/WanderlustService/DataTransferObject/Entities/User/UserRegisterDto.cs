using System.ComponentModel.DataAnnotations;

namespace WanderlustService.DataTransferObject.Entities.User
{
    /// <summary>
    /// Data transfer object for a new user
    /// </summary>
    public class UserRegisterDto
    {
        /// <summary>
        /// A username
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Username { get; set; }

        /// <summary>
        /// User's password in plaintext
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }

        /// <summary>
        /// User's email address
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }
    }
}
