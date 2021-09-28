using WanderlustService.DataTransferObject.Filter.Common;

namespace WanderlustService.DataTransferObject.Filter
{
    /// <summary>
    /// Filter used for storing user selection criteria
    /// </summary>
    public class UserFilterDto : FilterDtoBase
    {
        /// <summary>
        /// The sought username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The sought email
        /// </summary>
        public string Email { get; set; }
    }
}
