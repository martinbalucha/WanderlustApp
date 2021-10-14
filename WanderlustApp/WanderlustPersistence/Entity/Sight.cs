using System.ComponentModel.DataAnnotations;
using WanderlustInfrastructure.Entity;
using WanderlustPersistence.Enums;

namespace WanderlustPersistence.Entity
{
    /// <summary>
    /// Represents a tourist sight in a region
    /// </summary>
    public class Sight : EntityBase
    {
        /// <summary>
        /// The name of the sight
        /// </summary>
        [Required, MaxLength(150)]
        public string Name { get; set; }

        /// <summary>
        /// A region where the sight is located
        /// </summary>
        [Required]
        public RegionComponent Region { get; set; }

        /// <summary>
        /// A description of the sight
        /// </summary>
        [MaxLength(2000)]
        public string Description { get; set; }

        /// <summary>
        /// The type of the sight
        /// </summary>
        public SightType SightType { get; set; }

        /// <summary>
        /// Tells whether the sight is on the Unesco World Heritage List
        /// </summary>
        public bool IsUnescoSight { get; set; }
    }
}
