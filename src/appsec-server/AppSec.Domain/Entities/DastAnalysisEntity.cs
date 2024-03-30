using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace AppSec.Domain.Entities;

public class DastAnalysisEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string UrlDast { get; set; } = "";
    public string UserDast { get; set; } = "";

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public Collection<DastReport> DastReports { get; set; } = new Collection<DastReport>();
}
