using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppSec.Domain.DTOs
{
    public class UIDTO
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
        [JsonPropertyName("url")]
        public string URL { get; set; } = "";
        [JsonPropertyName("title")]
        public string Title { get; set; } = "";
        [JsonPropertyName("description")]
        public string Description { get; set; } = "";
        [JsonPropertyName("image")]
        public string Image { get; set; } = "";
        [JsonPropertyName("show")]
        public bool Show { get; set; } = false;

    }
}
