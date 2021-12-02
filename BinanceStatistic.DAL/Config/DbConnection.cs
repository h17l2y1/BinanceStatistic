using System.IO;
using BinanceStatistic.DAL.Config.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BinanceStatistic.DAL.Config
{
    public static class DbConnection
    {
        public static void Add(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options => { options.UseSqlServer(connectionString); });

            services.Configure<ConnectionStrings>(x => configuration.GetSection("ConnectionStrings").Bind(x));
        }
    }

    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetParent(@Directory.GetCurrentDirectory()) + "/BinanceStatistic.Api/appsettings.json")
                .Build();
            
            string connectionString = configuration.GetConnectionString("DefaultConnection");
    
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlServer(connectionString);
    
            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}