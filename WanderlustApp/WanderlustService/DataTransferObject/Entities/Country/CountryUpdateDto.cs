using System;
using System.ComponentModel.DataAnnotations;

namespace WanderlustService.DataTransferObject.Entities.Country
{
    /// <summary>
    /// DTO for updating a country
    /// </summary>
    public class CountryUpdateDto
    {
        /// <summary>
        /// ID of a country
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// The name of a country
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        /// <summary>
        /// The code of a country
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Code { get; set; }

        /// <summary>
        /// The description of a country
        /// </summary>
        [MaxLength(2500)]
        public string Description { get; set; }
    }
}
