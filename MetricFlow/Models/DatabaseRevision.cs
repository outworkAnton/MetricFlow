using System;

namespace MetricFlow.Models
{
    public class DatabaseRevision
    {
        public string Id { get; }
        public long Size { get; }
        public DateTime Modified { get; }

        public DatabaseRevision(string id, long size, string modified)
        {
            Id = id;
            Size = size;
            Modified = DateTime.Parse(modified);
        }

        public DatabaseRevision()
        { }
    }
}