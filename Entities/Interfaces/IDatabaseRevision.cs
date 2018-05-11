using System;

namespace Entities.Interfaces
{
    public interface IDatabaseRevision
    {
        string Id { get; set; }
        DateTime Modified { get; set; }
        long Size { get; set; }
    }
}