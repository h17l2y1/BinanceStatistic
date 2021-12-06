using BinanceStatistic.Telegram.BLL.Helpers;
using BinanceStatistic.Telegram.BLL.Helpers.Interfaces;
using BinanceStatistic.Telegram.BLL.Jobs;
using BinanceStatistic.Telegram.BLL.Services;
using BinanceStatistic.Telegram.BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BinanceStatistic.Telegram.BLL.Config
{
    public static class BusinessLogicDependencies
    {
        public static void Add(IServiceCollection services)
        {
            services.AddScoped<ITelegramBotService, TelegramBotService>();
            services.AddScoped<ISubscribeHelper, SubscribeHelper>();
            services.AddScoped<ISenderService, SenderService>();
            
            services.AddHostedService<SenderJob>();
        }
    }
}