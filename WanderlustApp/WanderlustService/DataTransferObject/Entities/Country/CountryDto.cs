
namespace WanderlustService.DataTransferObject.Entities.Country
{
    /// <summary>
    /// DTO designed for the fetching of the countries
    /// </summary>
    public class CountryDto
    {
        /// <summary>
        /// ID of the country
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// The name of the country
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The code of the country
        /// </summary>
        public string Code { get; set; }
    }
}
