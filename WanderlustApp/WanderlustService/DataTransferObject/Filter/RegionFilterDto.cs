using System;
using WanderlustService.DataTransferObject.Filter.Common;

namespace WanderlustService.DataTransferObject.Filter
{
    /// <summary>
    /// A filter for searching region components
    /// </summary>
    public class RegionFilterDto : FilterDtoBase
    {
        /// <summary>
        /// The name of the region
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The name of the country region belongs to
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string User { get; set; } 

        /// <summary>
        /// Determines whether the user has visited the region
        /// </summary>
        public bool? Visited { get; set; }
    }
}
