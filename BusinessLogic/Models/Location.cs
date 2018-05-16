using BusinessLogic.BusinessLogic.Contract.Models;
using BusinessLogic.Contract;

namespace BusinessLogic.Models
{
    public class Location : ILocation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Active { get; set; }
    }
}