﻿namespace BusinessLogic.BusinessLogic.Contract.Models
{
    public interface IService
    {
        string Id { get; set; }
        string Name { get; set; }
        string LocationId { get; set; }
        int Active { get; set; }
    }
}