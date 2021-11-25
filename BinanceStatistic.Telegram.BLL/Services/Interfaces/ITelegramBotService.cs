using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace BinanceStatistic.Telegram.BLL.Services.Interfaces
{
    public interface ITelegramBotService
    {
        Task Update(Update update);
    }
}