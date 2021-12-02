using System.Collections.Generic;
using System.Linq;
using BinanceStatistic.DAL.Entities;
using Telegram.Bot.Types.ReplyMarkups;

namespace BinanceStatistic.Telegram.BLL.Commands
{
    public class InlineMenu
    {
        protected InlineKeyboardMarkup GetMenuInline(ICollection<UserSubscribe> userSubscribes)
        {
            var menu = new InlineKeyboardMarkup(
                new InlineKeyboardButton[][]
                {
                    new InlineKeyboardButton[]
                    {
                        GetInlineNameWithEmoji(userSubscribes, Constants.Constants.Button.Inline.MIN5),
                        GetInlineNameWithEmoji(userSubscribes, Constants.Constants.Button.Inline.MIN15)
                    },
                    new InlineKeyboardButton[]
                    {
                        GetInlineNameWithEmoji(userSubscribes, Constants.Constants.Button.Inline.MIN30),
                        GetInlineNameWithEmoji(userSubscribes, Constants.Constants.Button.Inline.MIN60)
                    }
               }
            );
            return menu;
        }

        private string GetInlineNameWithEmoji(ICollection<UserSubscribe> userSubscribes, string minutes)
        {
            var isSubscribeExist = userSubscribes.Any(a => a.Subscribe.Name == minutes);
            return isSubscribeExist ? $"{minutes} ✅" : $"{minutes} 🚫";
        }
    }
}