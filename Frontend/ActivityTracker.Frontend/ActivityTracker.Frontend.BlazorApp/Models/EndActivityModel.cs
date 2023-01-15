using Newtonsoft.Json;

namespace ActivityTracker.Frontend.BlazorApp.Models
{
    public class EndActivityModel
    {
        /// <summary>
        /// The id of the activity
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The start time of the activity.
        /// </summary>
        [JsonProperty("time")]
        public DateTime Time { get; set; }
    }
}
