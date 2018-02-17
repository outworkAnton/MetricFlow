using System;

namespace MetricFlow.Models
{
    public class DatabaseRevision
    {
        public string RevisioId { get; }
        public DateTime Modified { get; }
        public int Size { get; }

        public DatabaseRevision(string revisioId, DateTime modified, int size)
        {
            RevisioId = revisioId;
            Modified = modified;
            Size = size;
        }

        public DatabaseRevision()
        { }
    }
}