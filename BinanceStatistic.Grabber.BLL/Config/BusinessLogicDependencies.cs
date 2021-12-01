using BinanceStatistic.BinanceClient;
using BinanceStatistic.BinanceClient.Interfaces;
using BinanceStatistic.Grabber.BLL.Jobs;
using BinanceStatistic.Grabber.BLL.Services;
using BinanceStatistic.Grabber.BLL.Services.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace BinanceStatistic.Grabber.BLL.Config
{
    public static class BusinessLogicDependencies
    {
        public static void Add(IServiceCollection services)
        {
            services.AddScoped<IBinanceHttpClient, BinanceHttpClient>();
            services.AddScoped<IRequestSender, RequestSender>();
            services.AddScoped<IBinanceClient, Client>();
            services.AddScoped<IBinanceGrabberService, BinanceGrabberService>();
            
            services.AddHostedService<GrabberJob>();
        }
    }
}