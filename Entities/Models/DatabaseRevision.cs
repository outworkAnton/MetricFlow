using System;
using Entities.Interfaces;

namespace Entities.Models
{
    public class DatabaseRevision : IDatabaseRevision
    {
        public string Id { get; set; }
        public DateTime Modified { get; set; }
        public long Size { get; set; }
    }
}