using Newtonsoft.Json;

namespace ActivityTracker.Backend.FunctionApp.Models
{
    public class ActivityModel
    {
        /// <summary>
        /// The ID of the activity
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }


        /// <summary>
        /// The start time of the activity
        /// </summary>
        [JsonProperty("startTime")]
        public DateTime StartTime  { get; set; }

        /// <summary>
        /// The description of the activity
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// The end time of the activity
        /// </summary>
        [JsonProperty("endTime")]
        public DateTime? EndTime { get; set; }
    }
}
