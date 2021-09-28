
namespace WanderlustService.DataTransferObject.Filter.Common
{
    /// <summary>
    /// A base class for query filters
    /// </summary>
    public abstract class FilterDtoBase
    {
        /// <summary>
        /// The number of the page that was requested
        /// </summary>
        public int? RequestedPageNumber { get; set; }

        /// <summary>
        /// The size of one page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Sorting property
        /// </summary>
        public string SortProperty { get; set; }

        /// <summary>
        /// Determines whether the results should be fetched ascendingly or not
        /// </summary>
        public bool SortAscendingly { get; set; }
    }
}
