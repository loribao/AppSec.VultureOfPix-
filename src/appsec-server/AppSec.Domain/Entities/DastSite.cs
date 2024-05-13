using System.Text.Json.Serialization;

namespace AppSec.Domain.Entities;
public record Site(

        int id,
        [property: JsonPropertyName("@name")] string Name,

        [property: JsonPropertyName("@host")] string Host,

        [property: JsonPropertyName("@port")] string Port,

        [property: JsonPropertyName("@ssl")] string Ssl,

        [property: JsonPropertyName("alerts")] IReadOnlyList<Alert> Alerts
    );
