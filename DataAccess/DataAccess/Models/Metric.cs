using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Metric
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string ServiceId { get; set; }
        public int Active { get; set; }
    }
}