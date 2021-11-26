using BinanceStatistic.DAL.Repositories;
using BinanceStatistic.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BinanceStatistic.DAL.Config
{
    public static class DatabaseDependencies
    {
        public static void Add(IServiceCollection services)
        {
            services.AddTransient<IPositionRepository, PositionRepository>();
            services.AddTransient<ICurrencyRepository, CurrencyRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ISubscribeRepository, SubscribeRepository>();
            services.AddTransient<IUserSubscribeRepository, UserSubscribeRepository>();
        }
    }
}