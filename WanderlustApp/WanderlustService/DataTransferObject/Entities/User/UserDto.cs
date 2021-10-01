using System;
using System.Collections.Generic;
using System.Text;

namespace WanderlustService.DataTransferObject.Entities.User
{
    /// <summary>
    /// 
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
    }
}
