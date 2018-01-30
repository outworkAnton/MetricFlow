using System.Collections.Generic;
using MetricFlow.Interfaces;

namespace MetricFlow.Models
{
    public class Formula : IFormula
    {
        public int FormulaId { get; set; }
        public string FormulaName { get; set; }

        public Formula(int formulaId, string formulaName)
        {
            FormulaId = formulaId;
            FormulaName = formulaName;
        }

        public T Calculate<T>(IList<T> arguments)
        {
            throw new System.NotImplementedException();
        }
    }
}