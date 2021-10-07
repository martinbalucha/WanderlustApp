using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WanderlustInfrastructure.Entity;
using WanderlustPersistence.Enums;

namespace WanderlustPersistence.Entity
{
    /// <summary>
    /// A user registered in the system
    /// </summary>
    public class User : EntityBase
    {
        /// <summary>
        /// A username
        /// </summary>
        [Required, MaxLength(100)]
        public string Username { get; set; }

        /// <summary>
        /// Password's hash
        /// </summary>
        [Required, MaxLength(100)]
        public string Password { get; set; }

        /// <summary>
        /// Hash's salt
        /// </summary>
        [Required, MaxLength(100)]
        public string Salt { get; set; }

        /// <summary>
        /// The user's email address
        /// </summary>
        [Required, MaxLength(100), EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// User's role in the system
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// A set of countries the user has visited
        /// </summary>
        public ISet<Country> CountriesVisited { get; set; }

        /// <summary>
        /// A set of regions and subgregions the user has visited
        /// </summary>
        public ISet<RegionComponent> RegionsVisited { get; set; }

        /// <summary>
        /// A set of towns the user has visited
        /// </summary>
        public ISet<Town> TownsVisited { get; set; }

        /// <summary>
        /// A set of foods the user has tasted
        /// </summary>
        public ISet<TraditionalFood> TraditionalFoodsEaten { get; set; }
    }
}
