using System.Collections.Generic;

namespace WanderlustPersistence.Entity
{
    /// <summary>
    /// Represents a region of the country
    /// </summary>
    public class Region : RegionComponent
    {
        /// <summary>
        /// A collection of subregions in the 
        /// </summary>
        public ISet<RegionComponent> Subregions { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override void Add(RegionComponent subregion)
        {
            Subregions.Add(subregion);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override void Remove(RegionComponent subregion)
        {
            Subregions.Remove(subregion);
        }
    }
}
