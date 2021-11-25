using System.Threading.Tasks;
using BinanceStatistic.Telegram.BLL.Commands.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BinanceStatistic.Telegram.BLL.Commands
{
    public class ErrorCommand : ICommand
    {
        public bool Contains(string command)
        {
            throw new System.NotImplementedException();
        }

        public async Task Execute(Update update, ITelegramBotClient client)
        {
            string error = "Some Error";
            
            if (update.Message?.Text == null)
            {
                error = "Message.Text is empty";
            }
            
            await client.SendTextMessageAsync(update.Message.Chat.Id,
                error,
                null,
                null,
                null,
                null,
                null,
                null,
                null
            );
        }
    }
}