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
            DateTime dateTime = DateTime.UtcNow;
            CreationDate = new DateTime(dateTime.Ticks - (dateTime.Ticks % TimeSpan.TicksPerSecond), dateTime.Kind);
        }

        [Key]
        public string Id { get; set; }

        public DateTime CreationDate { get; set; }
    }
}