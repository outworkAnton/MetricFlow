using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Statistic
    {
        [Key]
        public string Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string LocationId { get; set; }
        public string ServiceId { get; set; }
        public string MetricId { get; set; }
        public string FormulaId { get; set; }
        public decimal Value { get; set; }
    }
}