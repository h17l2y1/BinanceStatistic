using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BinanceStatistic.DAL.Entities;
using BinanceStatistic.Telegram.BLL.Commands.Interfaces;
using BinanceStatistic.Telegram.BLL.Helpers.Interfaces;
using BinanceStatistic.Telegram.BLL.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BinanceStatistic.Telegram.BLL.Commands
{
    public class CreateSubscribeCommand : InlineMenu, ICommand
    {
        private readonly ISubscribeHelper _subscribeHelper;
        private readonly List<string> _minutes;

        public CreateSubscribeCommand(ISubscribeHelper subscribeHelper)
        {
            _subscribeHelper = subscribeHelper;
            _minutes = new List<string>
            {
                Constants.Constants.Button.Inline.MIN5,
                Constants.Constants.Button.Inline.MIN15,
                Constants.Constants.Button.Inline.MIN30,
                Constants.Constants.Button.Inline.MIN60
            };
        }
        
        public bool Contains(string command)
        {
            return _minutes.FirstOrDefault(f => f.Contains(command.Remove(command.Length-2).Trim())) != null;
        }

        public async Task Execute(Update update, ITelegramBotClient client)
        {
            Test test = await _subscribeHelper.CreateOrRemoveSubscribe(update.CallbackQuery.From.Id, update.CallbackQuery?.Data);
            string createOrRemoveOutputMessage = test.IsCreated ? $"You subscribed ✅ to the {update.CallbackQuery?.Data} newsletter" :
                                                                  $"You unsubscribed 🚫 from {update.CallbackQuery?.Data} newsletter";

            await client.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id,
                createOrRemoveOutputMessage,
                ParseMode.MarkdownV2,
                null,
                true,
                true,
                null,
                null,
                GetMenuInline(test.UserSubscribes)
            );
        }
    }
}