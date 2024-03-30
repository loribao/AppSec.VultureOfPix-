
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace AppSec.Domain.Entities;
public class Instance
{

    [Key]
    public int Id { get; set; }
    [JsonPropertyName("uri")]
    public string uri { get; set; }

    [JsonPropertyName("method")]
    public string method { get; set; }

    [JsonPropertyName("param")]
    public string param { get; set; }

    [JsonPropertyName("attack")]
    public string attack { get; set; }

    [JsonPropertyName("evidence")]
    public string evidence { get; set; }

    [JsonPropertyName("otherinfo")]
    public string otherinfo { get; set; }
}
