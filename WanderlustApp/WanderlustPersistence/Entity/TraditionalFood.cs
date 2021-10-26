using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WanderlustInfrastructure.Entity;

namespace WanderlustPersistence.Entity
{
    /// <summary>
    /// Represents a traditional food typical for a country
    /// </summary>
    public class TraditionalFood : EntityBase
    {
        /// <summary>
        /// A name of the food
        /// </summary>
        [Required(AllowEmptyStrings = false), MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// The food's description
        /// </summary>
        [MaxLength(2000)]
        public string Description { get; set; }

        /// <summary>
        /// Recipe for the food
        /// </summary>
        [MaxLength(2000)]
        public string Recipe { get; set; }

        /// <summary>
        /// A collection of countries the food is
        /// </summary>
        public ISet<Country> Country { get; set; }

        /// <summary>
        /// A set of users who have eaten the food
        /// </summary>
        public ISet<User> EatenByUsers { get; set; }
    }
}
