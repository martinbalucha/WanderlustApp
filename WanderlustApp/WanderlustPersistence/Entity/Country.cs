using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WanderlustInfrastructure.Entity;

namespace WanderlustPersistence.Entity
{
    /// <summary>
    /// Represents a country on Earth
    /// </summary>
    public class Country : EntityBase
    {
        /// <summary>
        /// A full name of the country
        /// </summary>
        [Required, MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// A code of the country
        /// </summary>
        [Required, MaxLength(10)]
        public string Code { get; set; }

        /// <summary>
        /// Countrie's description
        /// </summary>
        [MaxLength(2500)]
        public string Description { get; set; }

        /// <summary>
        /// A set of regions
        /// </summary>
        public ISet<RegionComponent> Regions { get; set; }

        /// <summary>
        /// A set of foods typical for the country
        /// </summary>
        public ISet<TraditionalFood> TypicalFoods { get; set; }

        /// <summary>
        /// A set of users who have visited the country
        /// </summary>
        public ISet<User> VisitedByUsers { get; set; }
    }
}
