namespace FranchisService.Models.Response
{
    /// <summary>
    /// Represents a paged result set with metadata.
    /// </summary>
    /// <typeparam name="T">Type of items in the result set.</typeparam>
    public class PagedResult<T>
    {
        /// <summary>
        /// The items for the current page.
        /// </summary>
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();

        /// <summary>
        /// The total number of items available.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// The current page number (1-based).
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// The size of each page.
        /// </summary>
        public int PageSize { get; set; }
    }
}
