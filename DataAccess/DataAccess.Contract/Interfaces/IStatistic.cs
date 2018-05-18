namespace DataAccess.Contract.Interfaces
{
    public interface IStatistic
    {
        string Id { get; set; }
        int Year { get; set; }
        int Month { get; set; }
        string LocationId { get; set; }
        string ServiceId { get; set; }
        string MetricId { get; set; }
        string FormulaId { get; set; }
        decimal Value { get; set; }
    }
}