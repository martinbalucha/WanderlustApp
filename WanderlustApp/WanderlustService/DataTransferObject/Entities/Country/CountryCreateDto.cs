using System.ComponentModel.DataAnnotations;

namespace WanderlustService.DataTransferObject.Entities.Country
{
    /// <summary>
    /// DTO for a new country
    /// </summary>
    public class CountryCreateDto
    {
        /// <summary>
        /// The name of the country
        /// </summary>
        [Required(AllowEmptyStrings = false), MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// The code of the country
        /// </summary>
        [Required(AllowEmptyStrings = false), MaxLength(10)]
        public string Code { get; set; }

        /// <summary>
        /// Country's description
        /// </summary>
        [MaxLength(2500)]
        public string Description { get; set; }
    }
}
