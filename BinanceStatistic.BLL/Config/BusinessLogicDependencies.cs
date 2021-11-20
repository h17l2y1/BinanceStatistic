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
            services.AddScoped<IBinanceConcurrentRequestsHttpClient, BinanceConcurrentRequestsHttpClient>();
            services.AddScoped<IBinanceHttpClient, BinanceHttpClient>();
            services.AddScoped<IBinanceClient, BinanceClient>();
            services.AddScoped<IBinanceService, BinanceService>();
        }
    }
}