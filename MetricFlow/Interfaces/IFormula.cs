using System.Collections.Generic;

namespace MetricFlow.Interfaces
{
    public interface IFormula
    {
        int FormulaId { get; set; }
        string FormulaName { get; set; }
        T Calculate<T>(IList<T> arguments);
    }
}