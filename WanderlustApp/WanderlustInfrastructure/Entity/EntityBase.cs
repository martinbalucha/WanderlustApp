using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
    }
}
