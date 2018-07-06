using System;

namespace BusinessLogic.Contract.Interfaces
{
    public interface IDatabaseRevision
    {
        string Id { get; set; }
        DateTime Modified { get; set; }
        long Size { get; set; }
        int Changed { get; set; }
    }
}