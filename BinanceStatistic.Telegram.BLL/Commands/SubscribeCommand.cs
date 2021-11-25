using System.Threading.Tasks;
using BinanceStatistic.Telegram.BLL.Commands.Interfaces;
using BinanceStatistic.Telegram.BLL.Constants;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
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
            await client.SendTextMessageAsync(update.Message.Chat.Id,
                MessageConstant.ABOUT_SUBSCRIBE,
                ParseMode.MarkdownV2,
                null,
                true,
                true,
                null,
                null,
                // GetMenu()
                GetMenuInline()
                );
        }
        
        private InlineKeyboardMarkup GetMenuInline()
        {
            InlineKeyboardMarkup menu = new InlineKeyboardMarkup(
                new InlineKeyboardButton[][]
                {
                    new InlineKeyboardButton[]
                    {
                        MessageConstant.MIN5, 
                        MessageConstant.MIN15, 
                        MessageConstant.MIN30
                    },
                });
            
            return menu;
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