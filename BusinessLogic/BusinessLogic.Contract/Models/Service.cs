namespace BusinessLogic.Contract.Models
{
    public class Service
    {
        public Service(string id, string name, string locationId, int active)
        {
            Id = id;
            Name = name;
            LocationId = locationId;
            Active = active;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string LocationId { get; set; }
        public int Active { get; set; }
    }
}