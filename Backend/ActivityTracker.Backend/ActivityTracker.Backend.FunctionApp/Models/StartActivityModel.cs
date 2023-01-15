using Newtonsoft.Json;

namespace ActivityTracker.Backend.FunctionApp.Models
{
    public class StartActivityModel
    {
        /// <summary>
        /// The start time of the activity.
        /// </summary>
        [JsonProperty("time")]
        public DateTime Time { get; set; }

        /// <summary>
        /// The description of the activity
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
