using BusinessLogic.Contract.Interfaces;

namespace BusinessLogic.Models
{
    public class Metric : IMetric
    {
        public Metric(string id, string name, string serviceId, int active)
        {
            this.Id = id;
            this.Name = name;
            this.ServiceId = serviceId;
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
        public string ServiceId
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