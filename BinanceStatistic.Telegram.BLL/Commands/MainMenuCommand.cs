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
            return command.Contains(ButtonConstant.TO_MAIN_MENU);
        }

        public async Task Execute(Update update, ITelegramBotClient client)
        {
            // Get user id for subscriptions
            // var user = update.
            
            await client.SendTextMessageAsync(update.Message.Chat.Id,
                MessageConstant.ABOUT_SUBSCRIBE,
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
                    new KeyboardButton[] { ButtonConstant.SUBSCRIBE },
                    new KeyboardButton[] { MessageConstant.BACK_TO_MENU },
                })
            {
                ResizeKeyboard = true
            };
            
            return menu;
        }
    }
}