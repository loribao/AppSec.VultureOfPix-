
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AppSec.Domain.Entities;

public class DastReport
{

    [Key]
    public int Id { get; set; }

    [JsonPropertyName("@programName")]
    public string programName { get; set; }

    [JsonPropertyName("@version")]
    public string version { get; set; }

    [JsonPropertyName("@generated")]
    public string generated { get; set; }

    [JsonPropertyName("site")]
    public List<Site> site { get; set; }
}
