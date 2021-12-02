using System.Threading.Tasks;
using BinanceStatistic.DAL.Repositories.Interfaces;
using BinanceStatistic.Telegram.BLL.Commands.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using User = BinanceStatistic.DAL.Entities.User;

namespace BinanceStatistic.Telegram.BLL.Commands
{
    public class SubscribeCommand : InlineMenu, ICommand
    {
        private readonly IUserRepository _userRepository;

        public SubscribeCommand(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public bool Contains(string command)
        {
            return command.Contains(Constants.Constants.Button.Keyboard.Subscribe);
        }

        public async Task Execute(Update update, ITelegramBotClient client)
        {
            User user = await _userRepository.GetUserWithSubscriptions(update.Message.From.Id);

            if (user != null)
            {
                await client.SendTextMessageAsync(update.Message.Chat.Id,
                    Constants.Constants.Message.AboutSubscribe,
                    ParseMode.MarkdownV2,
                    null,
                    true,
                    true,
                    null,
                    null,
                    GetMenuInline(user.UserSubscribes)
                );
            }

        }
    }
}