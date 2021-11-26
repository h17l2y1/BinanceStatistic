using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BinanceStatistic.DAL.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace BinanceStatistic.DAL.Config
{
    public static class DataSeeder
    {
        public static void Seed(IServiceCollection services)
        {
            GetDbContext(services);
            SeedData(services);
        }

        private static void GetDbContext(IServiceCollection services)
        {
            ServiceProvider serviceProvider = services.BuildServiceProvider();

            using (var context = serviceProvider.GetRequiredService<ApplicationContext>())
            {
                if (((RelationalDatabaseCreator) context.Database.GetService<IDatabaseCreator>()).Exists())
                {
                    SeedData(services);
                }
            }
        }

        private static void SeedData(IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            SeedSubscribes(serviceProvider).Wait();
        }

        private static async Task SeedSubscribes(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationContext>();
            if (!context.Subscribes.Any())
            {
                List<Subscribe> leagues = new List<Subscribe>
                {
                    new Subscribe("5 min", 5),
                    new Subscribe("15 min", 15),
                    new Subscribe("30 min", 30),
                    new Subscribe("60 min", 60)
                };

                await context.AddRangeAsync(leagues);
                await context.SaveChangesAsync();
            }
        }
    }
}