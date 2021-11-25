using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace BinanceStatistic.Telegram.BLL.Config
{
    public static class TelegramBot
    {
        public static void Add(IServiceCollection services, IConfiguration configuration)
        {
            string webHook = configuration["WebHook"];
            string telegramEndpoint = configuration["BotEndpoint"];
            string telegramWebHook = $"{webHook}{telegramEndpoint}";
            
            ITelegramBotClient telegramClient = new TelegramBotClient(configuration["Token"]);
            telegramClient.SetWebhookAsync(telegramWebHook);
            services.AddSingleton(telegramClient);
        }
    }
}