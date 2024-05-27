using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppSec.Infra.Data.DataAcess.OwaspZap.Model;

public class ZapReportDto
{
    [JsonPropertyName("@programName")]
    public string programName { get; set; }

    [JsonPropertyName("@version")]
    public string version { get; set; }

    [JsonPropertyName("@generated")]
    public string generated { get; set; }

    [JsonPropertyName("site")]
    public List<SiteDto> site { get; set; }
}

public class AlertDto
{
    [JsonPropertyName("pluginid")]
    public string pluginid { get; set; }

    [JsonPropertyName("alertRef")]
    public string alertRef { get; set; }

    [JsonPropertyName("alert")]
    public string alert { get; set; }

    [JsonPropertyName("name")]
    public string name { get; set; }

    [JsonPropertyName("riskcode")]
    public int riskcode { get; set; }

    [JsonPropertyName("confidence")]
    public int confidence { get; set; }

    [JsonPropertyName("riskdesc")]
    public string riskdesc { get; set; }

    [JsonPropertyName("desc")]
    public string desc { get; set; }

    [JsonPropertyName("instances")]
    public List<InstanceDto> instances { get; set; }

    [JsonPropertyName("count")]
    public int count { get; set; }

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

public class InstanceDto
{
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

public class SiteDto
{
    [JsonPropertyName("@name")]
    public string name { get; set; }

    [JsonPropertyName("@host")]
    public string host { get; set; }

    [JsonPropertyName("@port")]
    public string port { get; set; }

    [JsonPropertyName("@ssl")]
    public string ssl { get; set; }

    [JsonPropertyName("alerts")]
    public List<AlertDto> alerts { get; set; }
}