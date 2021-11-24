using AutoMapper;
using BinanceStatistic.BLL.Config.MapperProfiles;
using Microsoft.Extensions.DependencyInjection;

namespace BinanceStatistic.BLL.Config
{
    public static class AutoMapper
    {
        public static void Add(IServiceCollection services)
        {
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile(new CurrencyProfile());
                c.AddProfile(new StatisticProfile());
            });

            IMapper mapper = config.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}