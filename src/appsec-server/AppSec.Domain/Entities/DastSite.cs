
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AppSec.Domain.Entities;

public class Site
{

    [Key]
    public int Id { get; set; }
    [JsonPropertyName("@name")]
    public string name { get; set; }

    [JsonPropertyName("@host")]
    public string host { get; set; }

    [JsonPropertyName("@port")]
    public string port { get; set; }

    [JsonPropertyName("@ssl")]
    public string ssl { get; set; }

    [JsonPropertyName("alerts")]
    public List<Alert> alerts { get; set; }
}
