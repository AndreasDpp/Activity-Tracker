namespace ActivityTracker.Frontend.BlazorApp.Models
{
    public class PagedDataModel
    {
        /// <summary>
        /// The current page
        /// </summary>
        public int CurrentPage { get; set; }
        
        /// <summary>
        /// The total amount of pages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// The page size
        /// </summary>
        public int PageSize { get; set; }
        
        /// <summary>
        /// The total amount of items
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Has a previous page
        /// </summary>
        public bool HasPrevious { get; set; }

        /// <summary>
        /// Has a next page
        /// </summary>
        public bool HasNext { get; set; }
    }
}
