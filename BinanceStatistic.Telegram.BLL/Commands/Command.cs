using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BinanceStatistic.Telegram.BLL.Commands
{
    public abstract class Command
    {
        public virtual async Task<bool> Contains(string command)
        {
            return true;
        }

        public abstract Task Execute(Message message, ITelegramBotClient client);
    }
}