using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BinanceStatistic.Telegram.BLL.Commands.Interfaces
{
    public interface ICommand
    {
        bool Contains(string command);
        
        Task Execute(Update update, ITelegramBotClient client);
    }
}