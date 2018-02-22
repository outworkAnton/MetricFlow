using System;

namespace MetricFlow.Models
{
    public class DatabaseRevision
    {
        public string Id { get; }
        public DateTime Modified { get; }
        public int Size { get; }

        public DatabaseRevision(string id, DateTime modified, int size)
        {
            Id = id;
            Modified = modified;
            Size = size;
        }

        public DatabaseRevision()
        { }
    }
}