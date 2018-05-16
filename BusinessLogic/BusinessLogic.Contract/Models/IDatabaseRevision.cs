using System;

namespace BusinessLogic.BusinessLogic.Contract.Models
{
    public interface IDatabaseRevision
    {
        string Id { get; set; }
        DateTime Modified { get; set; }
        long Size { get; set; }
    }
}