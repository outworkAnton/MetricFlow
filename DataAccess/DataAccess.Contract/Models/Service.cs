using DataAccess.Contract.Interfaces;

namespace DataAccess.Contract.Models
{
    public class Service : IService
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LocationId { get; set; }
        public int Active { get; set; }
    }
}