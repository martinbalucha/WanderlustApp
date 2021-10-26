using System;
using System.ComponentModel.DataAnnotations;
using WanderlustInfrastructure.Entity;

namespace WanderlustPersistence.Entity
{
    /// <summary>
    /// A class for user's visit of a region
    /// </summary>
    public class RegionVisit : EntityBase
    {
        private const int RatingLowerBound = 1;
        private const int RatingUpperBound = 5;

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public User User { get; set; }

        [Required]
        public Region Region { get; set; }

        /// <summary>
        /// Date of the 
        /// </summary>
        public DateTime? DateOfVisit { get; set; }

        /// <summary>
        /// User's comment of the visit
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Range(RatingLowerBound, RatingUpperBound)]
        public int Rating { get; set; }
    }
}
