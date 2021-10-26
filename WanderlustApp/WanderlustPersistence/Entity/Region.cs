using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WanderlustInfrastructure.Entity;

namespace WanderlustPersistence.Entity
{
    /// <summary>
    /// Represents a region
    /// </summary>
    public class Region : EntityBase
    {
        /// <summary>
        /// The name of the region
        /// </summary>
        [Required(AllowEmptyStrings = false), MaxLength(50)]
        public string Name;

        /// <summary>
        /// Description of the region
        /// </summary>
        [MaxLength(2000)]
        public string Description { get; set; }

        /// <summary>
        /// A set of countries the region belongs to
        /// </summary>
        [Required]
        public Country Country { get; set; }

        /// <summary>
        /// A set of users who have visited the region
        /// </summary>
        public ISet<User> VisitedByUsers { get; set; }        

        /// <summary>
        /// A set of towns located in the region
        /// </summary>
        public ISet<Town> Towns { get; set; }
    }
}
