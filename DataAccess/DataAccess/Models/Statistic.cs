﻿using System.ComponentModel.DataAnnotations;
using DataAccess.Contract.Interfaces;

namespace DataAccess.Models
{
    public class Statistic : IStatistic
    {
        [Key]
        public string Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string LocationId { get; set; }
        public string ServiceId { get; set; }
        public string MetricId { get; set; }
        public string FormulaId { get; set; }
        public decimal Value { get; set; }
    }
}