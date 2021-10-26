using System.ComponentModel.DataAnnotations;
using WanderlustInfrastructure.Entity;
using WanderlustPersistence.Enums;

namespace WanderlustPersistence.Entity
{
    /// <summary>
    /// Represents type of the sight
    /// </summary>
    public class SightType : EntityBase
    {
        /// <summary>
        /// Shows the origin of the sight
        /// </summary>
        public SightOrigin SightOrigin { get; set; }

        /// <summary>
        /// The name of the sight type
        /// </summary>
        [Required(AllowEmptyStrings = false), MaxLength(40)]
        public string Name { get; set; }
    }
}
