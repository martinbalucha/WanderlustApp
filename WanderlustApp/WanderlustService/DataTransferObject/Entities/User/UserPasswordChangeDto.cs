using System.ComponentModel.DataAnnotations;

namespace WanderlustService.DataTransferObject.Entities.User
{
    /// <summary>
    /// DTO for the user password change
    /// </summary>
    public class UserPasswordChangeDto
    {
        /// <summary>
        /// A username
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Username { get; set; }

        /// <summary>
        /// An old password in plaintext
        /// </summary>
        [Required]
        public string OldPassword { get; set; }

        /// <summary>
        /// A new password in plaintext
        /// </summary>
        [Required]
        public string NewPassword { get; set; }
    }
}
