using BusinessLogic.Contract.Interfaces;

namespace BusinessLogic.Models
{
    public class Statistic : IStatistic
    {
        public Statistic(string id, int year, int month, string locationId, string serviceId, string metricId, string formulaId, decimal value)
        {
            this.Id = id;
            this.Year = year;
            this.Month = month;
            this.LocationId = locationId;
            this.ServiceId = serviceId;
            this.MetricId = metricId;
            this.FormulaId = formulaId;
            this.Value = value;

        }
        public string Id
        {
            get;
            set;
        }
        public int Year
        {
            get;
            set;
        }
        public int Month
        {
            get;
            set;
        }
        public string LocationId
        {
            get;
            set;
        }
        public string ServiceId
        {
            get;
            set;
        }
        public string MetricId
        {
            get;
            set;
        }
        public string FormulaId
        {
            get;
            set;
        }
        public decimal Value
        {
            get;
            set;
        }
    }
}