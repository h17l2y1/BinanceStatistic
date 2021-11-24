using BinanceStatistic.Telegram.BLL.Services;
using BinanceStatistic.Telegram.BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BinanceStatistic.Telegram.BLL.Config
{
    public static class BusinessLogicDependencies
    {
        public static void Add(this IServiceCollection services)
        {
            services.AddScoped<ITelegramBotService, TelegramBotService>();
        }
    }
}