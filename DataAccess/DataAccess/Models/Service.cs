using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Service
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string LocationId { get; set; }
        public int Active { get; set; }
    }
}