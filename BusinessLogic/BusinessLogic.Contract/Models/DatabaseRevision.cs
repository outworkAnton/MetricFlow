using System;
using BusinessLogic.Contract.Interfaces;

namespace BusinessLogic.Contract.Models
{
    public class DatabaseRevision : IDatabaseRevision
    {
        public string Id { get; set; }
        public DateTime Modified { get; set; }
        public long Size { get; set; }
        public int Changed { get; set; }

        public DatabaseRevision(string id, DateTime modified, long size, int changed)
        {
            Id = id;
            Modified = modified;
            Size = size;
            Changed = changed;
        }
    }
}