
using System.Threading.Tasks;
using BinanceStatistic.Telegram.BLL.Commands.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BinanceStatistic.Telegram.BLL.Commands
{
    public class AboutCommand : ICommand
    {
        public bool Contains(string command)
        {
            return command.Contains(Constants.Constants.Button.Keyboard.About);
        }

        public async Task Execute(Update update, ITelegramBotClient client)
        {
            await client.SendTextMessageAsync(update.Message.Chat.Id,
                Constants.Constants.Message.About,
                null,
                null,
                null,
                null,
                null,
                null,
                GetMenu());
        }
        
        private ReplyKeyboardMarkup GetMenu()
        {
            ReplyKeyboardMarkup menu = new ReplyKeyboardMarkup(
                new KeyboardButton[][]
                {
                    new KeyboardButton[] { "None" },
                    new KeyboardButton[] { Constants.Constants.Button.Keyboard.Subscribe },
                    new KeyboardButton[] { Constants.Constants.Button.Keyboard.About },
                })
            {
                ResizeKeyboard = true
            };
            
            return menu;
        }
    }
}