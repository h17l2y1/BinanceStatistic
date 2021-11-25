using System.Collections.Generic;
using System.Threading.Tasks;
using BinanceStatistic.Telegram.BLL.Commands.Interfaces;
using BinanceStatistic.Telegram.BLL.Constants;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BinanceStatistic.Telegram.BLL.Commands
{
    public class WelcomeCommand : ICommand
    {
        public bool Contains(string command)
        {
            return command.ToLower().Contains(ButtonConstant.START);
        }

        public async Task Execute(Update update, ITelegramBotClient client)
        {
            // Get user id for subscriptions
            // var user = update.
            
            await client.SendTextMessageAsync(update.Message.Chat.Id,
                MessageConstant.WELCOME_MESSAGE,
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
            IEnumerable<KeyboardButton> keyboard = new List<KeyboardButton>
            {
                new KeyboardButton(ButtonConstant.TO_MAIN_MENU)
            };
            ReplyKeyboardMarkup menu = new ReplyKeyboardMarkup(keyboard);
            return menu;
        }
    }
}