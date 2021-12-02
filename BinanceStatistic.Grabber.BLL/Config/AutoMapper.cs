using AutoMapper;
using BinanceStatistic.Grabber.BLL.Config.MapperProfiles;
using Microsoft.Extensions.DependencyInjection;

namespace BinanceStatistic.Grabber.BLL.Config
{
    public static class AutoMapper
    {
        public static void Add(IServiceCollection services)
        {
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile(new CurrencyProfile());
            });

            IMapper mapper = config.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}