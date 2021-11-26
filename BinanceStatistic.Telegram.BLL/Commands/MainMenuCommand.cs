using System.Threading.Tasks;
using BinanceStatistic.Telegram.BLL.Commands.Interfaces;
using BinanceStatistic.Telegram.BLL.Constants;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BinanceStatistic.Telegram.BLL.Commands
{
    public class MainMenuCommand : ICommand
    {
        public bool Contains(string command)
        {
            return command.Contains(Constants.Constants.Button.Keyboard.ToMenu);
        }

        public async Task Execute(Update update, ITelegramBotClient client)
        {
            await client.SendTextMessageAsync(update.Message.Chat.Id,
                Constants.Constants.Button.Keyboard.ToMenu,
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