namespace DataAccess.Contract.Models
{
    public class Metric
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ServiceId { get; set; }
        public int Active { get; set; }
    }
}