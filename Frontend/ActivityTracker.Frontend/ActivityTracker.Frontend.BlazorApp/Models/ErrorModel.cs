using Newtonsoft.Json;

namespace ActivityTracker.Frontend.BlazorApp.Models
{
    public class ErrorModel
    {
        /// <summary>
        /// The error message.
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
