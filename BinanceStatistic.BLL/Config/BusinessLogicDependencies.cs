using BinanceStatistic.BLL.Services;
using BinanceStatistic.BLL.Services.Interface;
using BinanceStatistic.BinanceClient;
using BinanceStatistic.BinanceClient.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BinanceStatistic.BLL.Config
{
    public static class BusinessLogicDependencies
    {
        public static void Add(IServiceCollection services)
        {
            services.AddScoped<IBinanceHttpClient, BinanceHttpClient>();
            services.AddScoped<IRequestSender, RequestSender>();
            services.AddScoped<IBinanceClient, Client>();
            services.AddScoped<IBinanceService, BinanceService>();
       }
    }
}