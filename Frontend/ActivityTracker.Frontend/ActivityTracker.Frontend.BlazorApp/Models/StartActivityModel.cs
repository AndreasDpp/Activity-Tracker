using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ActivityTracker.Frontend.BlazorApp.Models
{
    public class StartActivityModel
    {
        /// <summary>
        /// The start time of the activity.
        /// </summary>
        [JsonProperty("time")]
        public DateTime? Time { get; set; }

        /// <summary>
        /// The description of the activity
        /// </summary>
        [Required]
        [StringLength(200, ErrorMessage = "Description is too long.")]
        [JsonProperty("Description")]
        public string? Description { get; set; }
    }
}
