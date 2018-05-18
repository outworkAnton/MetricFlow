using BusinessLogic.Contract.Interfaces;

namespace BusinessLogic.Contract.Models
{
    public class Formula : IFormula
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MetricId { get; set; }
        public int Active { get; set; }
    }
}