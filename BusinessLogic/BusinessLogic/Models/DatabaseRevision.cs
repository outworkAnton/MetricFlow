using System;
using BusinessLogic.Contract.Interfaces;

namespace BusinessLogic.Models
{
    public class DatabaseRevision : IDatabaseRevision
    {
        public string Id { get; set; }
        public DateTime Modified { get; set; }
        public long Size { get; set; }

        public DatabaseRevision(string id, DateTime modified, long size)
        {
            Id = id;
            Modified = modified;
            Size = size;
        }
    }
}