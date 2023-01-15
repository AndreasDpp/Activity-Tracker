using Newtonsoft.Json;

namespace ActivityTracker.Frontend.BlazorApp.Models
{
    public class ActivityModel
    {
        /// <summary>
        /// The id of the activity
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The start time of the activity
        /// </summary>
        [JsonProperty("startTime")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// The description of the activity
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// The calculation of the duration of the activity.
        /// </summary>
        public TimeSpan? Duration
        {
            get
            {
                if(EndTime == null)
                {
                    return TimeSpan.Zero;
                }

                var diff = EndTime - StartTime;
                return diff;
            }
        }

        /// <summary>
        /// The end time of the activity
        /// </summary>
        [JsonProperty("endTime")]
        public DateTime? EndTime { get; set; }
    }
}
