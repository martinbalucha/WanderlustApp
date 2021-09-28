using System.ComponentModel.DataAnnotations;

namespace WanderlustService.DataTransferObject.Entities.User
{
    /// <summary>
    /// Data transfer object for the user who is attemting to log into the system
    /// </summary>
    public class UserLoginDto
    {
        /// <summary>
        /// A username
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Username { get; set; }

        /// <summary>
        /// A password
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}
