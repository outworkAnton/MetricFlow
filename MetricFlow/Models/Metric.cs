using System.Collections.Generic;
using MetricFlow.Interfaces;

namespace MetricFlow.Models
{
    public class Metric : IMetric
    {
        public int MetricId { get; set; }
        public string MetricName { get; set; }
        public IEnumerable<IFormula> Formulas { get; set; }

        public Metric(int metricId, string metricName, IEnumerable<IFormula> formulas)
        {
            MetricId = metricId;
            MetricName = metricName;
            Formulas = formulas;
        }
    }
}