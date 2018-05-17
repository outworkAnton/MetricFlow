using System;

namespace DataAccess.Contract
{
    public interface IDatabaseRevision
    {
        string Id { get; set; }
        DateTime Modified { get; set; }
        long Size { get; set; }
    }
}