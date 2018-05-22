﻿using DataAccess.Contract.Interfaces;

namespace DataAccess.Contract.Models
{
    public class Metric : IMetric
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ServiceId { get; set; }
        public int Active { get; set; }
    }
}