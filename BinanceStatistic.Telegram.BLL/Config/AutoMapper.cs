using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace BinanceStatistic.Telegram.BLL.Config
{
    public static class AutoMapper
    {
        public static void Add(IServiceCollection services)
        {
            var config = new MapperConfiguration(c =>
            {
                // c.AddProfile(new StatisticProfile());
            });

            IMapper mapper = config.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}