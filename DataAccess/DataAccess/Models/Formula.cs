using System.ComponentModel.DataAnnotations;
using DataAccess.Contract.Interfaces;

namespace DataAccess.Models
{
    public class Formula : IFormula
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string MetricId { get; set; }
        public int Active { get; set; }
    }
}