using System.Collections.Generic;
using System.Threading.Tasks;
using BinanceStatistic.Telegram.BLL.Constants;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BinanceStatistic.Telegram.BLL.Commands
{
    public class WelcomeCommand : Command
    {
        public override async Task<bool> Contains(string command)
        {
            return command.ToLower().Contains(ButtonConstant.START);
        }

        public async override Task Execute(Message message, ITelegramBotClient client)
        {
            // await client.SendTextMessageAsync(message.Chat.Id, MessageConstant.WELCOME_MESSAGE, ParseMode.Html, null,false, false, 0, GetMenu());
            await client.SendTextMessageAsync(message.Chat.Id, MessageConstant.WELCOME_MESSAGE);
        }

        private ReplyKeyboardMarkup GetMenu()
        {
            IEnumerable<KeyboardButton> keyboard = new List<KeyboardButton> { new KeyboardButton(MessageConstant.START) };
            ReplyKeyboardMarkup menu = new ReplyKeyboardMarkup(keyboard);
            return menu;
        }
    }
}