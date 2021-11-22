using BinanceStatistic.BLL.Helpers;
using BinanceStatistic.BLL.Helpers.Interfaces;
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
            services.AddScoped<IBinanceHttpClient, BinanceHttpClient>();
            services.AddScoped<IRequestSender, RequestSender>();
            services.AddScoped<IBinanceClient, BinanceClient>();
            services.AddScoped<IBinanceService, BinanceService>();
            services.AddScoped<IPositionHelper, PositionHelper>();
        }
    }
}