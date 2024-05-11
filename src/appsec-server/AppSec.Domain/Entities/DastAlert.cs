using System.Text.Json.Serialization;
namespace AppSec.Domain.Entities;

public record Alert(

        int alertId,

        [property: JsonPropertyName("pluginid")] string Pluginid,

        [property: JsonPropertyName("alertRef")] string AlertRef,

        [property: JsonPropertyName("alert")] string AlertName,

        [property: JsonPropertyName("name")] string Name,

        [property: JsonPropertyName("riskcode")] string Riskcode,

        [property: JsonPropertyName("confidence")] string Confidence,

        [property: JsonPropertyName("riskdesc")] string Riskdesc,

        [property: JsonPropertyName("desc")] string Desc,

        [property: JsonPropertyName("instances")] IReadOnlyList<Instance> Instances,

        [property: JsonPropertyName("count")] string Count,

        [property: JsonPropertyName("solution")] string Solution,

        [property: JsonPropertyName("otherinfo")] string Otherinfo,

        [property: JsonPropertyName("reference")] string Reference,

        [property: JsonPropertyName("cweid")] string Cweid,

        [property: JsonPropertyName("wascid")] string Wascid,

        [property: JsonPropertyName("sourceid")] string Sourceid
     );
