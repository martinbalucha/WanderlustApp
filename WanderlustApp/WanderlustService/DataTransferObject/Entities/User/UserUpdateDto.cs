using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WanderlustService.DataTransferObject.Entities.User
{
    /// <summary>
    /// 
    /// </summary>
    public class UserUpdateDto 
    {
        [Required]
        public Guid Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Role
        /// </summary>
        public int Role { get; set; } 
    }
}
