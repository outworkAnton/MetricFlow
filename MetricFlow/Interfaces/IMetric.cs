using System.Collections.Generic;

namespace MetricFlow.Interfaces
{
    public interface IMetric
    {
        int MetricId { get; set; }
        string MetricName { get; set; }
        IEnumerable<IFormula> Formulas { get; set; }
    }
}