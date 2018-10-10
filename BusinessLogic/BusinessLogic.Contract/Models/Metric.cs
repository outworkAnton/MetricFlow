namespace BusinessLogic.Contract.Models
{
    public class Metric
    {
        public Metric(string id, string name, string serviceId, int active)
        {
            Id = id;
            Name = name;
            ServiceId = serviceId;
            Active = active;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string ServiceId { get; set; }
        public int Active { get; set; }
    }
}