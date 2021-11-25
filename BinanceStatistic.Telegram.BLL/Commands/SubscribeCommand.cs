using System.Threading.Tasks;
using BinanceStatistic.Telegram.BLL.Commands.Interfaces;
using BinanceStatistic.Telegram.BLL.Constants;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BinanceStatistic.Telegram.BLL.Commands
{
    public class SubscribeCommand : ICommand
    {
        public bool Contains(string command)
        {
            return command.Contains(ButtonConstant.SUBSCRIBE);
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
                    new KeyboardButton[] { MessageConstant.MIN5, MessageConstant.MIN15, MessageConstant.MIN30 },
                    new KeyboardButton[] { MessageConstant.BACK_TO_MENU },
                })
            {
                ResizeKeyboard = true
            };
            
            return menu;
        }
    }
}