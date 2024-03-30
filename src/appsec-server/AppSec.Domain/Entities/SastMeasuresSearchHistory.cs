using System.ComponentModel.DataAnnotations;

namespace AppSec.Domain.Entities;

public class SastMeasuresSearchHistory
{
    [Key]
    public int Id { get; set; } = 0;
    public string Name { get; set; }
    public IEnumerable<SastMeasuresSearchHistoryItem> History { get; set; }
}

public class SastMeasuresSearchHistoryItem
{
    [Key]
    public int Id { get; set; } = 0;
    public string Date { get; set; }
    public string Value { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
