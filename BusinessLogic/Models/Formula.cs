﻿using BusinessLogic.BusinessLogic.Contract.Models;
using BusinessLogic.Contract;

namespace BusinessLogic.Models
{
    public class Formula : IFormula
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MetricId { get; set; }
        public int Active { get; set; }
    }
}