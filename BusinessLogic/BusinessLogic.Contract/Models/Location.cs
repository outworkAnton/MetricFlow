using BusinessLogic.Contract.Interfaces;

namespace BusinessLogic.Contract.Models
{
    public class Location : ILocation
    {
        public Location(string id, string name, int active)
        {
            this.Id = id;
            this.Name = name;
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
        public int Active
        {
            get;
            set;
        }
    }
}