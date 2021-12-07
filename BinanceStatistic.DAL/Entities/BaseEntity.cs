using System;
using System.ComponentModel.DataAnnotations;
using BinanceStatistic.DAL.Entities.Interfaces;

namespace BinanceStatistic.DAL.Entities
{
    public class BaseEntity : IBaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
            CreationDate = DateTime.Now;
        }

        [Key]
        public string Id { get; set; }

        public DateTime CreationDate { get; set; }
    }
}