using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ActivityTracker.Backend.Repository.Domain.Documents.Base
{
    public class BaseDocument
    {
        [BsonId]
        [BsonElement("_id")]
        [JsonProperty("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
