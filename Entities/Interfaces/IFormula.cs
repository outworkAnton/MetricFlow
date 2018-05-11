﻿namespace Entities.Interfaces
{
    public interface IFormula
    {
        string Id { get; set; }
        string Name { get; set; }
        string MetricId { get; set; }
        int Active { get; set; }
    }
}