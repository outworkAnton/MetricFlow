using System;
using DataAccess.Contract.Interfaces;

namespace DataAccess.Contract.Models
{
    public class DatabaseRevision : IDatabaseRevision
    {
        public string Id { get; set; }
        public DateTime Modified { get; set; }
        public long Size { get; set; }
        public int Changed { get; set; }
    }
}