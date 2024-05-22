using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace AppSec.Domain.DTOs
{

    public class BaseComponent
    {
        [JsonPropertyName("key")]
        public string key { get; set; }

        [JsonPropertyName("name")]
        public string name { get; set; }

        [JsonPropertyName("qualifier")]
        public string qualifier { get; set; }

        [JsonPropertyName("measures")]
        public List<Measure> measures { get; set; }
    }

    public class Component
    {
        [JsonPropertyName("key")]
        public string key { get; set; }

        [JsonPropertyName("name")]
        public string name { get; set; }

        [JsonPropertyName("qualifier")]
        public string qualifier { get; set; }

        [JsonPropertyName("path")]
        public string path { get; set; }

        [JsonPropertyName("language")]
        public string language { get; set; }

        [JsonPropertyName("measures")]
        public List<Measure> measures { get; set; }
    }

    public class Measure
    {
        [JsonPropertyName("metric")]
        public string metric { get; set; }

        [JsonPropertyName("value")]
        public float value { get; set; }

        [JsonPropertyName("bestValue")]
        public bool? bestValue { get; set; }
    }

    public class Paging
    {
        [JsonPropertyName("pageIndex")]
        public int pageIndex { get; set; }

        [JsonPropertyName("pageSize")]
        public int pageSize { get; set; }

        [JsonPropertyName("total")]
        public int total { get; set; }
    }

    public class SastMesuaresComponentTreeDTO
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        
        [JsonPropertyName("paging")]
        public Paging paging { get; set; }

        [JsonPropertyName("baseComponent")]
        public BaseComponent baseComponent { get; set; }

        [JsonPropertyName("components")]
        public List<Component> components { get; set; }
        public DateTime DateRun { get; set; } = DateTime.Now;

        public string _guid { get; set; } = Guid.NewGuid().ToString();
    }

}
