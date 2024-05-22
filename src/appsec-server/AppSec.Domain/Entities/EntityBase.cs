namespace AppSec.Domain.Entities
{
    public class EntityBase
    {
        public string Id { get; set; }
        public DateTime LastUpdate { get; set; } = DateTime.Now;
    }
}
