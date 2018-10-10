namespace BusinessLogic.Contract.Models
{
    public class Formula
    {
        public Formula(string id, string name, string metricId, int active)
        {
            Id = id;
            Name = name;
            MetricId = metricId;
            Active = active;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string MetricId { get; set; }
        public int Active { get; set; }
    }
}