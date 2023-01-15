using ActivityTracker.Backend.Service.DTO;

namespace ActivityTracker.Backend.Service.Inferfaces
{
    public interface IActivityService
    {
        /// <summary>
        /// Starts a new activity
        /// </summary>
        /// <param name="startDTO">Start Activity DTO</param>
        /// <returns>The new created activity</returns>
        Task<ActivityDTO> StartAsync(StartActivityDTO startDTO);

        /// <summary>
        /// Ends a activity
        /// </summary>
        /// <param name="endDTO">End Activity DTO</param>
        /// <returns>The update activity</returns>
        Task<ActivityDTO> EndAsync(EndActivityDTO endDTO);

        /// <summary>
        /// Gets a single activity
        /// </summary>
        /// <param name="id">The id of the activity</param>
        /// <returns>The activity</returns>
        Task<ActivityDTO> GetAsync(string id);

        /// <summary>
        /// Gets a paged list of activities
        /// </summary>
        /// <param name="pageNumber">The page number</param>
        /// <param name="pageSize">The page size</param>
        /// <returns>A list of activities</returns>
        Task<PagedListDTO<ActivityDTO>> GetPagedAsync(int pageNumber, int pageSize = 20);

    }
}
