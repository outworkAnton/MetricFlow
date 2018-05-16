using DataAccess.Contract;

namespace DataAccess.Models
{
    public class Location : ILocation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Active { get; set; }
    }
}