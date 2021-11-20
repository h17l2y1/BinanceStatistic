using BinanceStatistic.BLL.Services;
using BinanceStatistic.BLL.Services.Interface;
using BinanceStatistic.Core;
using BinanceStatistic.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BinanceStatistic.BLL.Config
{
    public static class BusinessLogicDependencies
    {
        public static void Add(IServiceCollection services)
        {
            services.AddScoped<IBaseBinanceConcurrentRequestsHttpClient, BinanceHttpClient>();
            services.AddScoped<IBaseBinanceHttpClient, BaseBinanceHttpClient>();
            services.AddScoped<IBinanceClient, BinanceClient>();
            services.AddScoped<IBinanceService, BinanceService>();
        }
    }
}