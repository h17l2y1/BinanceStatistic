using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace BinanceStatistic.Telegram.BLL.Config
{
    public static class TelegramBot
    {
        public static void Add(IServiceCollection services, IConfiguration configuration)
        {
            ITelegramBotClient telegramClient = new TelegramBotClient(configuration["Token"]);
            services.AddSingleton(telegramClient);
        }
    }
}