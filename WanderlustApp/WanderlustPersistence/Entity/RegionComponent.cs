using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WanderlustInfrastructure.Entity;

namespace WanderlustPersistence.Entity
{
    /// <summary>
    /// Represents a region. Serves as an interface for implementing Composite design pattern
    /// for the regions of Europe.
    /// </summary>
    public abstract class RegionComponent : EntityBase
    {
        /// <summary>
        /// The name of the region
        /// </summary>
        [Required]
        public string Name;

        /// <summary>
        /// A set of countries the region belongs to
        /// </summary>
        public ISet<Country> Country { get; set; }

        /// <summary>
        /// A set of users who have visited the region
        /// </summary>
        public ISet<User> VisitedByUsers { get; set; }

        /// <summary>
        /// Description of the region
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Adds a new subregion to the current region
        /// </summary>
        /// <param name="subregion">A subregion that is to be added to the region</param>
        public virtual void Add(RegionComponent subregion)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes a subregion from the region
        /// </summary>
        /// <param name="subregion">A subregion that will be removed from the region</param>
        public virtual void Remove(RegionComponent subregion)
        {
            throw new NotImplementedException();
        }
    }
}
