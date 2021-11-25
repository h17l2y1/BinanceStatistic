using System;
using System.Threading.Tasks;
using BinanceStatistic.Telegram.BLL.Commands.Interfaces;
using BinanceStatistic.Telegram.BLL.Constants;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BinanceStatistic.Telegram.BLL.Commands
{
    // public class Command : ICommand
    // {
    //     public bool Contains(string command)
    //     {
    //         return true;
    //     }
    //
    //     public async Task Execute(Message message, MessageConstant constant, ITelegramBotClient client, ReplyKeyboardMarkup menu)
    //     {
    //         await client.SendTextMessageAsync(message.Chat.Id,
    //             constant.ToString() ?? string.Empty,
    //             null, null, null, null,
    //             null, null,
    //             menu);
    //     }
    // }
}