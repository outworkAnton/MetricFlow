using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Contract.Interfaces;

namespace DataAccess.Models
{
    public class DatabaseRevision : IDatabaseRevision
    {
        [Key]
        public string Id { get; set; }
        public DateTime Modified { get; set; }
        public long Size { get; set; }
    }
}