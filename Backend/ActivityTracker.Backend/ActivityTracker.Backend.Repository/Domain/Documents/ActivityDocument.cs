using ActivityTracker.Backend.Repository.Domain.Context;
using ActivityTracker.Backend.Repository.Domain.Documents.Base;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ActivityTracker.Backend.Repository.Domain.Documents
{
    [BsonCollection("activities")]
    public class ActivityDocument: BaseDocument
    {
        [BsonElement("start")]
        [JsonProperty("start")]
        public DateTime Start { get; set; }

        [BsonElement("description")]
        [JsonProperty("description")]
        public string Description { get; set; }

        [BsonElement("end")]
        [JsonProperty("end")]
        public DateTime? End { get; set; }

    }
}
