using System;
using System.ComponentModel.DataAnnotations;

namespace WanderlustInfrastructure.Entity
{
    /// <summary>
    /// Represents an entity that can be stored in the database
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// ID of an entity
        /// </summary>
        [Key]
        public Guid Id { get; set; }
    }
}
