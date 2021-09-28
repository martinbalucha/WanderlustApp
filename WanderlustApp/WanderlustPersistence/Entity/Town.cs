using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WanderlustInfrastructure.Entity;

namespace WanderlustPersistence.Entity
{
    /// <summary>
    /// Represents a town that has a significance
    /// </summary>
    public class Town : EntityBase
    {
        /// <summary>
        /// A region which the town belongs to
        /// </summary>
        [Required]
        public RegionComponent Region { get; set; }

        /// <summary>
        /// The name of the town
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Description of the town
        /// </summary>
        [MaxLength(2000)]
        public string Description { get; set; }

        /// <summary>
        /// A collection of sights located in the town
        /// </summary>
        public IEnumerable<Sight> Sights { get; set; }

        /// <summary>
        /// A set of users who visited the town
        /// </summary>
        public ISet<User> VisitedByUsers { get; set; }
    }
}
