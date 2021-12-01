using BinanceStatistic.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BinanceStatistic.DAL.Config
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
		
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<UserSubscribe> UserSubscribes { get; set; }
    }
}