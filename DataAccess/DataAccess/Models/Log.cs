using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Log
    {
        [Key]
        public string TimeStamp { get; set; }
        public string Callsite { get; set; }
        public string Logger { get; set; }
        public string Loglevel { get; set; }
        public string Message { get; set; }
    }
}