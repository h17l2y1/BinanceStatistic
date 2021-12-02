using System.Threading.Tasks;
using BinanceStatistic.DAL.Repositories.Interfaces;
using BinanceStatistic.Telegram.BLL.Commands.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using User = BinanceStatistic.DAL.Entities.User;

namespace BinanceStatistic.Telegram.BLL.Commands
{
    public class WelcomeCommand : ICommand
    {
        private readonly IUserRepository _userRepository;

        public WelcomeCommand(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Contains(string command)
        {
            return command.ToLower().Contains(Constants.Constants.Button.Keyboard.Start);
        }

        public async Task Execute(Update update, ITelegramBotClient client)
        {
            await CreateUser(update);
            await client.SendTextMessageAsync(update.Message.Chat.Id,
                Constants.Constants.Message.Welcome,
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
            var menu = new ReplyKeyboardMarkup(new KeyboardButton(Constants.Constants.Button.Keyboard.ToMenu))
            {
                ResizeKeyboard = true
            };

            return menu;
        }

        private async Task CreateUser(Update update)
        {
            var user = new User
            {
                TelegramId = update.Message.From.Id,
                FirstName = update.Message.From.FirstName,
                UserName = update.Message.From.Username,
                Language = update.Message.From.LanguageCode,
                ChatId = update.Message.Chat.Id,
            };

            User dbUser = await _userRepository.FindUserByTelegramId(user.TelegramId);
            if (dbUser == null)
            {
                await _userRepository.Create(user);
            }
        }
    }
}