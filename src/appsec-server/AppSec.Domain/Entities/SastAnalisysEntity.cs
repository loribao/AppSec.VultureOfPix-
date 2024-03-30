using AppSec.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace AppSec.Domain.Entities;

public class SastAnalisysEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string UrlBase { get; set; } = "";
    public string User { get; set; } = "";
    public string Password { get; set; } = "";
    public string Token { get; set; } = "";
    public Languages Languages { get; set; } = Languages.CSharp;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public virtual IEnumerable<SastMeasuresSearchHistory> Measures { get; set; } = new List<SastMeasuresSearchHistory>();
}
