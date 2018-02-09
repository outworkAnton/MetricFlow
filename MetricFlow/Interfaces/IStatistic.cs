namespace MetricFlow.Interfaces
{
    public interface IStatistic
    {
        int Id { get; set; }
        int Year { get; set; }
        int Month { get; set; }
        int LocationId { get; set; }
        int ServiceId { get; set; }
        int MetricId { get; set; }
        int? FormulaId { get; set; }
        decimal Value { get; set; }
    }
}