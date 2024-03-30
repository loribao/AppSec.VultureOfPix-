namespace AppSec.Infra.DTos;

//Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class History
{
    public string date { get; set; }
    public string value { get; set; }
}

public class Measure
{
    public string metric { get; set; }
    public List<History> history { get; set; }
}

public class Paging
{
    public int pageIndex { get; set; }
    public int pageSize { get; set; }
    public int total { get; set; }
}

public class DtoMeasuresSearchHistory
{
    public Paging paging { get; set; }
    public List<Measure> measures { get; set; }
}

