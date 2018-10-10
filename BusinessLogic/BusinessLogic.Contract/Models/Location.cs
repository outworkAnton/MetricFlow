namespace BusinessLogic.Contract.Models
{
    public class Location
    {
        public Location(string id, string name, int active)
        {
            Id = id;
            Name = name;
            Active = active;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int Active { get; set; }
    }
}