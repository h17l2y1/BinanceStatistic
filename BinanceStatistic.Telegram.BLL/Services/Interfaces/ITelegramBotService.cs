using System.Threading.Tasks;
using BinanceStatistic.Telegram.BLL.Models;
using Telegram.Bot.Types;

namespace BinanceStatistic.Telegram.BLL.Services.Interfaces
{
    public interface ITelegramBotService
    {
        Task Update(Update update);

        Task<WebhookInfo> GetHookInfo();

        Task SendMessageToUsers(GetStatisticRequest request);
    }
}