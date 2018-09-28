using BusinessLogic.Contract.Interfaces;

namespace BusinessLogic.Models
{
    public class Service : IService
    {
        public Service(string id, string name, string locationId, int active)
        {
            this.Id = id;
            this.Name = name;
            this.LocationId = locationId;
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
        public string LocationId
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