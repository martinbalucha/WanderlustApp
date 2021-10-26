using System;
using WanderlustPersistence.Enums;

namespace WanderlustService.DataTransferObject.Entities.User
{
    /// <summary>
    /// A DTO for fetched users
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// User role
        /// </summary>
        public Role Role { get; set; }
    }
}
