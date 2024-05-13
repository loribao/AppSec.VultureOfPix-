using System.Text.Json.Serialization;
namespace AppSec.Domain.Entities;

public record Instance(
    int id,
    [property: JsonPropertyName("uri")] string Uri,
    [property: JsonPropertyName("method")] string Method,
    [property: JsonPropertyName("param")] string Param,
    [property: JsonPropertyName("attack")] string Attack,
    [property: JsonPropertyName("evidence")] string Evidence,
    [property: JsonPropertyName("otherinfo")] string Otherinfo
);
