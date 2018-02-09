using System;
using MetricFlow.Interfaces;

namespace MetricFlow.Models
{
    public class Statistic : IStatistic
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int LocationId { get; set; }
        public int ServiceId { get; set; }
        public int MetricId { get; set; }
        public int? FormulaId { get; set; }
        public decimal Value { get; set; }

        public Statistic(int id, int year, int month, int locationId, int serviceId, int metricId, int? formulaId = null, decimal value = decimal.Zero)
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

        public Statistic() { }
    }
}