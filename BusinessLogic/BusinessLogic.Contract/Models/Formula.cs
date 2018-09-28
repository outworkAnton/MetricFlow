using BusinessLogic.Contract.Interfaces;

namespace BusinessLogic.Contract.Models
{
    public class Formula : IFormula
    {
        public Formula(string id, string name, string metricId, int active)
        {
            this.Id = id;
            this.Name = name;
            this.MetricId = metricId;
            this.Active = active;

        }
        public string Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string MetricId
        {
            get;
            set;
        }
        public int Active
        {
            get;
            set;
        }
    }
}