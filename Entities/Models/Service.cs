﻿using Entities.Interfaces;

namespace Entities.Models
{
    public class Service : IService
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LocationId { get; set; }
        public int Active { get; set; }
    }
}