using BusinessLogic.Contract.Interfaces;

namespace BusinessLogic.Contract.Models
{
    public class Location : ILocation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Active { get; set; }
    }
}