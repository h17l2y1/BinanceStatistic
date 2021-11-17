using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace BinanceStatistic.BLL.Config
{
    public static class AutoMapper
    {
        public static void Add(IServiceCollection services)
        {
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile(new MapperProfile());
            });

            IMapper mapper = config.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}