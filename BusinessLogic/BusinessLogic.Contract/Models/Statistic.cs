namespace BusinessLogic.Contract.Models
{
    public class Statistic
    {
        public Statistic(string id, int year, int month, string locationId, string serviceId, string metricId,
            string formulaId, decimal value)
        {
            Id = id;
            Year = year;
            Month = month;
            LocationId = locationId;
            ServiceId = serviceId;
            MetricId = metricId;
            FormulaId = formulaId;
            Value = value;
        }

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