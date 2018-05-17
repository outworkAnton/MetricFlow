using System;
using DataAccess.Contract;

namespace DataAccess.Models
{
    public class DatabaseRevision : IDatabaseRevision
    {
        public string Id { get; set; }
        public DateTime Modified { get; set; }
        public long Size { get; set; }
    }
}