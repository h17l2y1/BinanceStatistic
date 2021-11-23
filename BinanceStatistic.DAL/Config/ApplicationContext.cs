using BinanceStatistic.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BinanceStatistic.DAL.Config
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
		
        public DbSet<Position> Positions { get; set; }
    }
}