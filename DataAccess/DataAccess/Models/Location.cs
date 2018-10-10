using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Location
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Active { get; set; }
    }
}