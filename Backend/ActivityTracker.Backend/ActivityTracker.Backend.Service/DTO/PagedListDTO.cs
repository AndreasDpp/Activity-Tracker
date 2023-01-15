namespace ActivityTracker.Backend.Service.DTO
{
    public class PagedListDTO<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        
        public int TotalPages { get; private set; }
        
        public int PageSize { get; private set; }
        
        public int TotalCount { get; private set; }
        
        public bool HasPrevious => CurrentPage > 1;

        public bool HasNext => CurrentPage < TotalPages;

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="items">Items of T</param>
        /// <param name="totalCount">Total number of items</param>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageSize">Page size</param>
        public PagedListDTO(List<T> items, int totalCount, int pageNumber, int pageSize)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            
            AddRange(items);
        }

        #endregion
    }
}
