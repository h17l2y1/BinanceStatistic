using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BinanceStatistic.Telegram.BLL.Commands.Interfaces;
using BinanceStatistic.Telegram.BLL.Helpers.Interfaces;
using BinanceStatistic.Telegram.BLL.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

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
            bool isContain = _minutes.FirstOrDefault(f =>
            {
                var text = command.Remove(command.Length - 2).Trim();
                if (text != "")
                {
                    return f.Contains(text);
                }
                return false;
            }) != null;
            return isContain;
        }

        public async Task Execute(Update update, ITelegramBotClient client)
        {
            Subscribe subscribe = await _subscribeHelper.CreateOrRemoveSubscribe(update.CallbackQuery.From.Id, update.CallbackQuery?.Data);
            string buttonName = update.CallbackQuery?.Data.Remove((int)update.CallbackQuery?.Data.Length - 2).Trim();
            string createOrRemoveOutputMessage = subscribe.IsCreated ? 
                                                                    $"Вы подписались на {buttonName}" :
                                                                    $"Вы отписались от {buttonName}";

            await client.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id,
                createOrRemoveOutputMessage,
                ParseMode.MarkdownV2,
                null,
                true,
                true,
                null,
                null,
                GetMenuInline(subscribe.UserSubscribes)
            );
        }
    }
}