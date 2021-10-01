using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WanderlustService.DataTransferObject.Entities.Country
{
    /// <summary>
    /// DTO for marking country as visited
    /// </summary>
    public class CountrySaveVisitDto
    {
        /// <summary>
        /// An ID of a country
        /// </summary>
        [Required]
        public Guid CountryId { get; set; }

        /// <summary>
        /// A name of the user who marked the country as visited
        /// </summary>
        [Required]
        public string Username { get; set; }
    }
}
