using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppSec.Infra.Data.DataAcess.SonarQube.Model;

public class DtoComponent
{
    [JsonPropertyName("key")]
    public string key { get; set; } = "";

    [JsonPropertyName("name")]
    public string name { get; set; } = "";

    [JsonPropertyName("qualifier")]
    public string qualifier { get; set; } = "";

    [JsonPropertyName("measures")]
    public List<DtoMeasure> measures { get; set; }
}

public class DtoMeasure
{
    [JsonPropertyName("metric")]
    public string metric { get; set; } = "";

    [JsonPropertyName("value")]
    public string value { get; set; } = "";

    [JsonPropertyName("bestValue")]
    public bool bestValue { get; set; }

    [JsonPropertyName("period")]
    public DtoPeriod? period { get; set; }
}

public class DtoMetric
{
    [JsonPropertyName("key")]
    public string key { get; set; } = "";

    [JsonPropertyName("name")]
    public string name { get; set; } = "";

    [JsonPropertyName("description")]
    public string description { get; set; } = "";

    [JsonPropertyName("domain")]
    public string domain { get; set; } = "";

    [JsonPropertyName("type")]
    public string type { get; set; } = "";

    [JsonPropertyName("higherValuesAreBetter")]
    public bool higherValuesAreBetter { get; set; }

    [JsonPropertyName("qualitative")]
    public bool qualitative { get; set; }

    [JsonPropertyName("hidden")]
    public bool hidden { get; set; }

    [JsonPropertyName("decimalScale")]
    public int? decimalScale { get; set; }

    [JsonPropertyName("bestValue")]
    public string bestValue { get; set; } = "";

    [JsonPropertyName("worstValue")]
    public string worstValue { get; set; } = "";
}

public class DtoPeriod
{
    [JsonPropertyName("index")]
    public int? index { get; set; }

    [JsonPropertyName("value")]
    public string value { get; set; } = "";

    [JsonPropertyName("bestValue")]
    public bool bestValue { get; set; }

    [JsonPropertyName("mode")]
    public string mode { get; set; } = "";

    [JsonPropertyName("date")]
    public DateTime? date { get; set; }
}

public class MeasuresComponentPeriod
{
    [JsonPropertyName("component")]
    public DtoComponent? component { get; set; }

    [JsonPropertyName("metrics")]
    public List<DtoMetric>? metrics { get; set; }

    [JsonPropertyName("period")]
    public DtoPeriod? period { get; set; }
}
