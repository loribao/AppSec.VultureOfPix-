using System.ComponentModel.DataAnnotations;

namespace AppSec.Domain.Entities
{
    public class EntityBase
    {
        [Key]
        public string Id { get; set; }
        public DateTime LastUpdate { get; set; } = DateTime.Now;
    }
}
