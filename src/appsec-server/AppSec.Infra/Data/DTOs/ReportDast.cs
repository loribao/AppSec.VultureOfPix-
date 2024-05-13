using System.Text.Json.Serialization;

namespace AppSec.Infra.Data.DTOs
{
    public record Alert(

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

    public record Instance(

        [property: JsonPropertyName("uri")] string Uri,
        [property: JsonPropertyName("method")] string Method,
        [property: JsonPropertyName("param")] string Param,
        [property: JsonPropertyName("attack")] string Attack,
        [property: JsonPropertyName("evidence")] string Evidence,
        [property: JsonPropertyName("otherinfo")] string Otherinfo
    );

    public record Root(

        [property: JsonPropertyName("@programName")] string ProgramName,

        [property: JsonPropertyName("@version")] string Version,

        [property: JsonPropertyName("@generated")] string Generated,

        [property: JsonPropertyName("site")] IReadOnlyList<Site> Site
    );

    public record Site(

        [property: JsonPropertyName("@name")] string Name,

        [property: JsonPropertyName("@host")] string Host,

        [property: JsonPropertyName("@port")] string Port,

        [property: JsonPropertyName("@ssl")] string Ssl,

        [property: JsonPropertyName("alerts")] IReadOnlyList<Alert> Alerts
    );
}
