using System.ComponentModel.DataAnnotations;

namespace WanderlustPersistence.Entity
{
    /// <summary>
    /// A subregion that has no further divisions
    /// </summary>
    public class Subregion : RegionComponent
    {
        /// <summary>
        /// The capital of the subregion either official or unofficial
        /// </summary>
        [Required]
        public Town Capital { get; set; }
    }
}
