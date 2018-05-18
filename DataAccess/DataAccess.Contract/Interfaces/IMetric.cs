namespace DataAccess.Contract.Interfaces
{
    public interface IMetric
    {
        string Id { get; set; }
        string Name { get; set; }
        string ServiceId { get; set; }
        int Active { get; set; }
    }
}