using System;
using System.Collections.Generic;
using System.Text;
using WanderlustService.DataTransferObject.Filter.Common;

namespace WanderlustService.DataTransferObject.Filter
{
    /// <summary>
    /// A filter DTO for countries
    /// </summary>
    public class CountryFilterDto : FilterDtoBase
    {
        /// <summary>
        /// A name of the country
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// An ID of the user who either visited or has not yet visited the country
        /// </summary>
        public long? UserId{ get; set; }

        /// <summary>
        /// An indicator whether the user has visited the country or not
        /// </summary>
        public bool? Visited { get; set; }
    }
}
