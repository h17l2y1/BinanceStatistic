using System.Collections.Generic;
using System.Threading.Tasks;
using BinanceStatistic.DAL.Repositories.Interfaces;
using BinanceStatistic.Telegram.BLL.Commands;
using BinanceStatistic.Telegram.BLL.Commands.Interfaces;
using BinanceStatistic.Telegram.BLL.Helpers.Interfaces;
using BinanceStatistic.Telegram.BLL.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BinanceStatistic.Telegram.BLL.Services
{
    public class TelegramBotService : ITelegramBotService
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IUserRepository _userRepository;
        private readonly ISubscribeHelper _subscribeHelper;
        private readonly ISenderService _senderService;
        private IEnumerable<ICommand> _commands;

        public TelegramBotService(ITelegramBotClient telegramClient, IUserRepository userRepository,
            ISubscribeHelper subscribeHelper, ISenderService senderService)
        {
            _telegramClient = telegramClient;
            _userRepository = userRepository;
            _subscribeHelper = subscribeHelper;
            _senderService = senderService;
            InitCommands();
        }

        public async Task<WebhookInfo> GetHookInfo()
        {
            await _senderService.Test5();
            
            WebhookInfo info = await _telegramClient.GetWebhookInfoAsync();
            return info;
        }

        public async Task Update(Update update)
        {
            if (update.Type == UpdateType.Message && update.Message?.Text != null ||
                update.Type == UpdateType.CallbackQuery && update.CallbackQuery?.Data != null)
            {
                string text = update.Type == UpdateType.Message ? update.Message?.Text : update.CallbackQuery?.Data;
                
                foreach (ICommand command in _commands)
                {
                    bool isCommandFound = command.Contains(text);
                    if (isCommandFound)
                    {
                        await command.Execute(update, _telegramClient);
                        break;
                    }
                }
            }
        }
        
        private void InitCommands()
        {
            _commands = new List<ICommand>
            {
                // new ErrorCommand(),
                new WelcomeCommand(_userRepository),
                new MainMenuCommand(),
                new SubscribeCommand(_userRepository),
                new CreateSubscribeCommand(_subscribeHelper),
                new AboutCommand(),
            };
        }
    }
}