using BusinessLogic.BusinessLogic.Contract.Models;
using BusinessLogic.Contract;

namespace BusinessLogic.Models
{
    public class Service : IService
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LocationId { get; set; }
        public int Active { get; set; }
    }
}