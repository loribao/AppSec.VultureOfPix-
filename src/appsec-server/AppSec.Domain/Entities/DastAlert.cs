using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace AppSec.Domain.Entities;
public class Alert
{
    [Key]
    public int Id { get; set; }

    [JsonPropertyName("pluginid")]
    public string pluginid { get; set; }

    [JsonPropertyName("alertRef")]
    public string alertRef { get; set; }

    [JsonPropertyName("alert")]
    public string alert { get; set; }

    [JsonPropertyName("name")]
    public string name { get; set; }

    [JsonPropertyName("riskcode")]
    public string riskcode { get; set; }

    [JsonPropertyName("confidence")]
    public string confidence { get; set; }

    [JsonPropertyName("riskdesc")]
    public string riskdesc { get; set; }

    [JsonPropertyName("desc")]
    public string desc { get; set; }

    [JsonPropertyName("instances")]
    public List<Instance> instances { get; set; }

    [JsonPropertyName("count")]
    public string count { get; set; }

    [JsonPropertyName("solution")]
    public string solution { get; set; }

    [JsonPropertyName("otherinfo")]
    public string otherinfo { get; set; }

    [JsonPropertyName("reference")]
    public string reference { get; set; }

    [JsonPropertyName("cweid")]
    public string cweid { get; set; }

    [JsonPropertyName("wascid")]
    public string wascid { get; set; }

    [JsonPropertyName("sourceid")]
    public string sourceid { get; set; }
}
