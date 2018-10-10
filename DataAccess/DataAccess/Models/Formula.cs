using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Formula
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string MetricId { get; set; }
        public int Active { get; set; }
    }
}